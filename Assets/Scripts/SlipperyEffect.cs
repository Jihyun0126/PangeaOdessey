using UnityEngine;
using UnityEngine.Tilemaps;

public class SlipperyEffect : MonoBehaviour
{
    public float normalFriction = 0.5f; // 기본 마찰력
    public float enhancedFriction = 0.1f; // 강화된 미끄러움의 마찰력
    public float timeToEnhance = 10.0f; // 미끄러움이 강화될 때까지의 시간
    public string slipperyMapName = "blue"; // 미끄러짐이 적용될 타일맵 이름

    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private PhysicsMaterial2D normalMaterial;
    private PhysicsMaterial2D enhancedMaterial;
    private float elapsedTime = 0f;
    private bool isEnhanced = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();

        // 물리 재질 생성
        normalMaterial = new PhysicsMaterial2D();
        normalMaterial.friction = normalFriction;
        enhancedMaterial = new PhysicsMaterial2D();
        enhancedMaterial.friction = enhancedFriction;

        // 초기 마찰력을 설정
        playerCollider.sharedMaterial = normalMaterial;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= timeToEnhance && !isEnhanced)
        {
            // 미끄러움을 강화
            playerCollider.sharedMaterial = enhancedMaterial;
            isEnhanced = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Tilemap>()?.name == slipperyMapName)
        {
            // 미끄러운 타일에 닿을 때 마찰력 적용
            if (!isEnhanced)
            {
                playerCollider.sharedMaterial = normalMaterial;
            }
            else
            {
                playerCollider.sharedMaterial = enhancedMaterial;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Tilemap>()?.name == slipperyMapName)
        {
            // 타일을 벗어날 때 기본 마찰력 적용
            if (!isEnhanced)
            {
                playerCollider.sharedMaterial = normalMaterial;
            }
            else
            {
                playerCollider.sharedMaterial = enhancedMaterial;
            }
        }
    }
}
