using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class IceTilemap : MonoBehaviour
{
    public Tilemap tilemap; // 타일맵 참조
    public float iceDrag = 0.1f;  // 미끄러움 정도를 나타내는 값
    public float activeTime = 5f;  // 빙판이 활성화되는 시간 (초)
    public float inactiveTime = 5f;  // 빙판이 비활성화되는 시간 (초)
    private bool isActive = true;
    private float timer = 0f;
    private List<Rigidbody2D> affectedRigidbodies = new List<Rigidbody2D>(); // 미끄러움 효과를 받은 리지드바디 목록

    void Update()
    {
        // 타이머 업데이트
        timer += Time.deltaTime;

        // 빙판 활성화/비활성화 조건 체크
        if (isActive && timer >= activeTime)
        {
            SetIceActive(false);
            timer = 0f;
        }
        else if (!isActive && timer >= inactiveTime)
        {
            SetIceActive(true);
            timer = 0f;
        }
    }

    void SetIceActive(bool active)
    {
        isActive = active;
        if (!isActive)
        {
            // 빙판이 비활성화될 때, 미끄러움 효과를 받은 모든 리지드바디의 drag를 원래대로 복원
            foreach (Rigidbody2D rb in affectedRigidbodies)
            {
                if (rb != null)
                {
                    rb.drag = 0f;  // 원래 drag 값 복원 (필요에 따라 조정)
                }
            }
            affectedRigidbodies.Clear(); // 목록 초기화
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isActive) return;  // 빙판이 비활성화 상태일 경우 종료

        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null && collision.gameObject.CompareTag("Ground"))
        {
            rb.drag = iceDrag;
            if (!affectedRigidbodies.Contains(rb))
            {
                affectedRigidbodies.Add(rb); // 미끄러움 효과를 받은 리지드바디를 목록에 추가
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (!isActive) return;  // 빙판이 비활성화 상태일 경우 종료

        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null && collision.gameObject.CompareTag("Ground"))
        {
            rb.drag = 0f;  // 원래 drag 값 복원 (필요에 따라 조정)
            affectedRigidbodies.Remove(rb); // 목록에서 제거
        }
    }
}
