using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public float damage = 10f; // 공격 시 데미지
    private bool playerInRange = false;
    private GameObject player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;
        }
    }

    public void ApplyAttackDamage()
    {
        if (playerInRange && player != null)
        {
            GameManager.instance.TakeDamage(damage);
        }
    }
}
