using UnityEngine;

public class IceTile : MonoBehaviour
{
    public float slipperyFriction = 0.1f; // 미끄러지는 효과로 변경될 마찰력 값

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 플레이어가 빙판 타일에 진입할 때 마찰력을 변경합니다.
            Rigidbody2D playerRigidbody = collision.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                playerRigidbody.sharedMaterial.friction = slipperyFriction; // 미끄러지는 효과로 변경될 마찰력 값으로 설정합니다.
            }
        }
    }
}
