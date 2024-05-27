using UnityEngine;

public class DamageTile : MonoBehaviour
{
    public float damageAmount = 10f; // 플레이어에게 입힐 데미지 양
    public float damageInterval = 2f; // 데미지를 입히는 간격
    public GameObject damageEffectPrefab; // 데미지 이펙트 프리팹

    private float nextDamageTime;

    void Start()
    {
        nextDamageTime = Time.time + damageInterval; // 초기 데미지를 입히는 시간 설정
    }

    void Update()
    {
        // 특정 시간마다 플레이어에게 데미지를 입힘
        if (Time.time >= nextDamageTime)
        {
            // 여기서 플레이어에게 데미지를 입히는 로직을 추가할 수 있음
            // 예를 들어, 플레이어의 health를 감소시키는 등의 작업을 수행

            // 다음 데미지를 입히는 시간 설정
            nextDamageTime = Time.time + damageInterval;

            // 데미지 이펙트 생성
            CreateDamageEffect();
        }
    }

    // 플레이어가 이 타일에 닿았을 때 호출되는 함수
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어에게 데미지를 입힘
            // 이 코드는 실제로 플레이어의 Health를 관리하는 방식에 따라 달라질 수 있음
            GameManager.instance.TakeDamage(damageAmount);

            // 데미지 이펙트 생성
            CreateDamageEffect();
        }
    }

    // 데미지 이펙트 생성
    void CreateDamageEffect()
    {
        if (damageEffectPrefab != null)
        {
            Instantiate(damageEffectPrefab, transform.position, Quaternion.identity);
        }
    }
}
