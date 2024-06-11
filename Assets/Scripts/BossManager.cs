// BossManager.cs
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform spawnPoint;
    public float bossSpawnDelay = 60f;
    public int bossMaxHP = 100;
    
    private int bossCurrentHP;
    private float timer = 0f;
    private bool bossSpawned = false;

    // HP Bar UI 및 이미지 표시 관련 변수들 선언

    void Update()
    {
        timer += Time.deltaTime;
        if (!bossSpawned && timer >= bossSpawnDelay)
        {
            SpawnBoss();
            // HP Bar UI를 활성화
        }
    }

    void SpawnBoss()
    {
        GameObject boss = Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
        bossCurrentHP = bossMaxHP;
        bossSpawned = true;
        // 보스 HP Bar 초기화 및 표시
    }

    public void TakeDamage(int damage)
    {
        bossCurrentHP -= damage;
        // HP Bar 감소 및 업데이트

        if (bossCurrentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // 보스 사망 처리 및 애니메이션 재생
        bossSpawned = false;
        timer = 0f;
        // HP Bar 비활성화
    }
}