using UnityEngine;

namespace Assets.PixelFantasy.PixelMonsters.Common.Scripts
{
    public class BossControls : MonoBehaviour
    {
        public Transform player;
        public float followDistance = 10f; // 플레이어를 추적할 최대 거리
        public float attackDistance = 3f; // 공격을 시작할 거리
        public float projectileDistance = 7f; // 프로젝타일을 발사할 거리
        public float runSpeed = 2f;
        public float attackCooldown = 1f; // 공격 간격
        public GameObject projectile; // 발사할 프로젝타일
        public float projectileSpeed = 3f; // 프로젝타일 속도

        private Animator animator;
        private float lastAttackTime;
        private bool facingRight = true;

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
                transform.position = Vector2.MoveTowards(transform.position, player.position, runSpeed * Time.deltaTime);
                animator.SetBool("Running", true);
            }
            else
            {
                animator.SetBool("Moving", false);
            }

            // 플레이어와의 거리가 프로젝타일을 발사할 거리보다 크면 발사
            if (distanceToPlayer > projectileDistance)
            {
                // 프로젝타일을 생성하고 발사
                FireProjectile();
            }

            // 플레이어 방향으로 회전
            if ((player.position.x > transform.position.x && !facingRight) || (player.position.x < transform.position.x && facingRight))
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

        void FireProjectile()
        {
            // 프로젝타일을 생성하고 발사
            GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            Vector2 direction = (player.position - transform.position).normalized; // 플레이어 위치로 향하는 벡터
            newProjectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;

            // 애니메이터에게 발사 애니메이션을 종료하도록 알림
            animator.ResetTrigger("Attack");
        }
    }
}
