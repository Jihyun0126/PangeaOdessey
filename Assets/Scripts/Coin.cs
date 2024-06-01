using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.tag == "Bullet")
            {
                gameObject.SetActive(true);
            }
            // 아이템을 먹었을 때의 로직 추가
            // 예: 점수 증가, 체력 회복 등
            // 여기서는 아이템 오브젝트를 비활성화
            gameObject.SetActive(false);
        }
    }
}
