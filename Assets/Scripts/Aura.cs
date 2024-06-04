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

        // ���� Ʈ���ŵ� �� ������Ʈ�� ������
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            // ���� �ӵ��� ���ҽ�Ŵ
            enemy.speed = 1f; // ���÷� 0.5��� ���ҽ�Ŵ
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        // ������ ��� �� ������Ʈ�� ������
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            // ���� �ӵ��� ������� ����
            //enemy.speed = enemy.originalSpeed;
        }
    }

    public void Init(float damage)
    {
        this.damage = damage;
    }
}
