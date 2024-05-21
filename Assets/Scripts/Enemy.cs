using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float originalSpeed; // 원래 속도를 저장할 변수
    public Rigidbody2D target;
    public Animator anim;
    public LayerMask playerLayer;
    public float detectionRadius = 5f;

    bool isLive = true;
    Rigidbody2D rigid;
    SpriteRenderer spriter;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();

        // 시작할 때 원래 속도 저장
        originalSpeed = speed;
    }

    void FixedUpdate()
    {
        if (!isLive) return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;

        // 플레이어 감지 및 공격
        DetectPlayerAndAttack();
    }

    void DetectPlayerAndAttack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                anim.SetTrigger("Attack");
                break;
            }
        }
    }

    void LateUpdate()
    {
        spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Aura"))
        {
            // 범위를 벗어나면 원래 속도로 복원
            speed = originalSpeed;
        }
    }
}
