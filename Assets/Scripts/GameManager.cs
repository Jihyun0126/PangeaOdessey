using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    public int bossMode = 0; // 0이면 일반맵, 1이면 보스맵
    [Header("# Player info")]
    public bool isLive;
    public static int bitCoin = 0;
    public float health = 100f; // int -> float
    public float maxHealth = 100f; // int -> float
    public float bossSpawnTime = 20f; // 보스 스폰 시간
    public int kill;

    [Header(" Game Object")]
    public PoolManager pool;
    public Player player;
    public Text gold;
    public Text timer;
    
    [Header("# Boss Info")]
    public GameObject bossPrefab; // 보스 프리팹
    public GameObject bossHUD; // 보스 HP UI
    public float spawnRadius = 5f; // 플레이어 주위 스폰 반경
    private bool bossSpawned = false; // 보스가 한 번만 스폰되도록 설정

    [Header("# Boss Health")]
    public float bossHealth;
    public float maxBossHealth;

    [Header("# Boss Damage")]
    public float bossDamageAmount = 10f;
    
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        health = maxHealth;
        bitCoin = 0;
        if (bossHUD != null)
        {
            bossHUD.SetActive(false); // 게임 시작 시 보스 HP UI 비활성화
        }
    }

    void Update()
    {
        gameTime += Time.deltaTime;
        
        if (bossMode == 1 && gameTime >= bossSpawnTime && !bossSpawned)
        {
            SpawnBoss();
        }
        
        if (gameTime < maxGameTime)
        {
            // 게임 오버 로직을 여기에 추가합니다.
            TimeSpan timeSpan = TimeSpan.FromSeconds(gameTime);
            string timeString = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
            timer.text = timeString;
            if (gameTime > maxGameTime) {
            
            }
        }
        gold.text = bitCoin.ToString()+ "G";
    }
    
    void SpawnBoss()
    {
        Vector2 spawnPosition = (Vector2)player.transform.position + UnityEngine.Random.insideUnitCircle * spawnRadius;
        GameObject boss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
        
        // 보스 체력 설정
        BossControls bossControls = boss.GetComponent<BossControls>();
        maxBossHealth = bossControls.health;
        bossHealth = maxBossHealth;

        if (bossHUD != null)
        {
            bossHUD.SetActive(true); // 보스가 스폰될 때 HP UI 활성화
        }
        bossSpawned = true; // 보스를 한 번만 스폰되도록 설정
    }
    

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health < 0) health = 0;
        Debug.Log("Health after damage: " + health);

        // 필요 시 플레이어가 죽었을 때 로직 추가
        if (health <= 0)
        {
            PlayerDead();
        }
    }

    void PlayerDead()
    {
        // 플레이어가 죽었을 때 처리할 로직을 여기에 추가합니다.
        //Debug.Log("Player is Dead. Game Over.");
        // 예: 게임 오버 화면 활성화, 게임 오버 사운드 재생 등
    }
    
    public void TakeBossDamage(float amount)
    {
        bossHealth -= amount;
        if (bossHealth < 0) bossHealth = 0;
        Debug.Log("Boss Health after damage: " + bossHealth);

        // 필요 시 보스가 죽었을 때 로직 추가
        if (bossHealth <= 0)
        {
            BossDead();
        }
    }

    void BossDead()
    {
        // 보스가 죽었을 때 처리할 로직을 여기에 추가합니다.
        // 예: 보스 사망 애니메이션 재생, 보스 관련 UI 비활성화 등
    }
}
