using UnityEngine;

public class BossControls : MonoBehaviour
{
    public Transform player;
    public float followDistance = 10f; // 플레이어를 추적할 최대 거리
    public float attackDistance = 3f; // 공격을 시작할 거리
    public float runSpeed = 2f;
    public float attackCooldown = 1f; // 공격 간격

    private Animator animator;
    private float lastAttackTime;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < attackDistance && Time.time > lastAttackTime + attackCooldown)
        {
            // 공격 애니메이션 트리거
            animator.SetTrigger("Attack");
            lastAttackTime = Time.time;
        }
        else if (distanceToPlayer < followDistance)
        {
            // 플레이어를 향해 이동
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * runSpeed * Time.deltaTime);
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        // 플레이어 방향으로 회전
        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
