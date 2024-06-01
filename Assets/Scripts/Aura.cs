using UnityEngine;

public class Aura : MonoBehaviour
{
    public float damage;
    private CircleCollider2D circleCollider;

    void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        // 현재 트리거된 적 오브젝트를 가져옴
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            // 적의 속도를 감소시킴
            enemy.speed = 1f; // 예시로 0.5배로 감소시킴
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        // 범위를 벗어난 적 오브젝트를 가져옴
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            // 적의 속도를 원래대로 복원
            //enemy.speed = enemy.originalSpeed;
        }
    }

    public void Init(float damage)
    {
        this.damage = damage;
    }
}
