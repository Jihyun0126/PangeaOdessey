using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f; // 게임 최대 시간
    public float bossSpawnTime = 20f; // 보스 스폰 시간

    [Header("# Player Info")]
    public float health = 100f;
    public float maxHealth = 100f;
    public int kill;

    [Header(" Game Object")]
    public PoolManager pool;
    public Player player;

    [Header("# Boss Info")]
    public GameObject bossPrefab; // 보스 프리팹
    public GameObject bossHUD; // 보스 HP UI
    public float spawnRadius = 5f; // 플레이어 주위 스폰 반경
    private bool bossSpawned = false; // 보스가 한 번만 스폰되도록 설정

    [Header("# Projectile Info")]
    public GameObject projectilePrefab; // 프로젝타일 프리팹
    public float projectileSpawnInterval = 2f; // 프로젝타일 생성 간격
    public float projectileSpawnDistance = 10f; // 프로젝타일 생성 거리
    private float lastProjectileSpawnTime;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        health = maxHealth;
        if (bossHUD != null)
        {
            bossHUD.SetActive(false); // 게임 시작 시 보스 HP UI 비활성화
        }
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime >= bossSpawnTime && !bossSpawned)
        {
            SpawnBoss();
        }

        if (gameTime > maxGameTime)
        {
            // 게임 오버 로직을 여기에 추가합니다.
        }

        // 플레이어와 보스의 거리를 계산하여 일정 거리 이상 멀어지면 projectile을 생성합니다.
        if (bossSpawned)
        {
            float distanceToPlayer = Vector2.Distance(player.transform.position, bossPrefab.transform.position);
            if (distanceToPlayer > projectileSpawnDistance && Time.time > lastProjectileSpawnTime + projectileSpawnInterval)
            {
                SpawnProjectile();
                lastProjectileSpawnTime = Time.time;
            }
        }
    }

    void SpawnBoss()
    {
        Vector2 spawnPosition = (Vector2)player.transform.position + Random.insideUnitCircle * spawnRadius;
        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);

        

        
        if (bossHUD != null)
        {
            bossHUD.SetActive(true); // 보스가 스폰될 때 HP UI 활성화
        }
        bossSpawned = true; // 보스를 한 번만 스폰되도록 설정
    }

    void SpawnProjectile()
    {
        Vector2 spawnPosition = bossPrefab.transform.position; // 보스 위치에서 생성
        Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health < 0) health = 0;
        Debug.Log("Health after damage: " + health);
    }
}
