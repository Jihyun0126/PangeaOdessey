using System.Collections;
using UnityEngine;

public class BurningDamage : MonoBehaviour
{
    public float damageAmount = 10f;       // 화상 데미지 양
    public float damageInterval = 1f;      // 데미지 간격
    private bool isBurning = false;        // 화상 상태 여부
    private Coroutine burnCoroutine;       // 코루틴 레퍼런스

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))    // Player 태그를 가진 객체가 트리거에 진입할 때
        {
            isBurning = true;
            burnCoroutine = StartCoroutine(ApplyBurningDamage(other));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))    // Player 태그를 가진 객체가 트리거에서 나갈 때
        {
            isBurning = false;
            if (burnCoroutine != null)
            {
                StopCoroutine(burnCoroutine);
            }
        }
    }

    IEnumerator ApplyBurningDamage(Collider player)
    {
        while (isBurning)
        {
            // 여기서 플레이어에게 데미지를 입히는 로직을 추가합니다.
            

            yield return new WaitForSeconds(damageInterval);
        }
    }
}
