using UnityEngine;

public class BossControls : MonoBehaviour
{
    public float followDistance = 10f; // 플레이어를 추적할 최대 거리
    public float attackDistance = 3f; // 공격을 시작할 거리
    public float runSpeed = 2f;
    public float attackCooldown = 1f; // 공격 간격
    public LayerMask playerLayer; // 플레이어 레이어 마스크
    public float detectionRadius = 5f; // 탐지 반경
    public float health = 100f; // 보스의 체력
    public float attackDamage = 10f; // 공격 시 데미지
    public float collisionDamage = 5f; // 충돌 시 데미지

    [Header("# Projectile Info")]
    public GameObject projectilePrefab; // 발사체 프리팹
    public float projectileSpawnInterval = 2f; // 발사체 생성 간격
    public float projectileSpawnDistance = 10f; // 발사체 생성 거리
    public float projectileDamage = 5f; // 발사체 데미지

    private Animator animator;
    private float lastAttackTime;
    private float lastProjectileSpawnTime;
    private bool facingRight = true;
    private bool isLive = true;
    private Rigidbody2D rigid;
    private Rigidbody2D player;
    private Vector2 lastPlayerPosition;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!isLive) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < attackDistance && Time.time > lastAttackTime + attackCooldown)
        {
            // 공격 애니메이션 트리거
            animator.SetTrigger("Attack");
            lastAttackTime = Time.time;
            GameManager.instance.TakeDamage(attackDamage);
        }
        else if (distanceToPlayer < followDistance)
        {
            // 플레이어를 향해 이동
            Vector2 dirVec = player.position - rigid.position;
            Vector2 nextVec = dirVec.normalized * runSpeed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);
            rigid.velocity = Vector2.zero;
            animator.SetBool("Running", true);

            // 플레이어의 마지막 위치 갱신
            lastPlayerPosition = player.position;
        }
        else
        {
            animator.SetBool("Running", false);
        }

        // 플레이어 방향으로 회전
        FlipTowardsPlayer();

        // 발사체 생성 조건 확인 및 생성
        if (distanceToPlayer > projectileSpawnDistance && Time.time > lastProjectileSpawnTime + projectileSpawnInterval)
        {
            SpawnProjectile();
            lastProjectileSpawnTime = Time.time;
        }
    }

    void FlipTowardsPlayer()
    {
        // 플레이어의 위치에 따라 보스의 방향을 변경
        if (player.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (player.position.x < transform.position.x && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void SpawnProjectile()
    {
        Vector2 spawnPosition = transform.position;
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.Initialize(lastPlayerPosition, projectileDamage);
        }
    }

    public void TakeDamage(float amount)
    {
        if (!isLive) return;

        health -= amount;
        GameManager.instance.UpdateBossHealth(health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isLive = false;
        animator.SetTrigger("Dead");
        // 보스가 죽은 후 처리할 로직 추가 (예: 일정 시간 후 제거)
        Destroy(gameObject, 2f); // 2초 후 오브젝트 제거
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.TakeDamage(collisionDamage);
        }
    }
}
