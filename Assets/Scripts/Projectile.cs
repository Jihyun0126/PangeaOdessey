using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 targetPosition;
    public float speed;
    public float damage; // 발사체의 데미지

    private bool facingRight = true;

    public void Initialize(Vector3 target, float damageAmount)
    {
        targetPosition = target;
        damage = damageAmount;

        // Set initial facing direction
        if (targetPosition.x < transform.position.x)
        {
            Flip();
        }
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if the projectile has reached the target position
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            Destroy(gameObject);
        }

        // Update the facing direction based on movement direction
        Vector3 direction = targetPosition - transform.position;
        if (direction.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.TakeDamage(damage);
            Destroy(gameObject); // 발사체 충돌 시 파괴
        }
    }
}
