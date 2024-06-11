using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject itemPrefab;
    SpriteRenderer sr;
    public float speed;
    public float originalspeed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;
    public Animator anim;
    public LayerMask playerLayer;
    public float detectionRadius = 5f;
    public AudioSource audioSource; // AudioSource ������Ʈ �߰�

    bool isLive;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;
    Collider2D coll;
    AudioClip deathAudioClip; // ���� �� ����� AudioClip

    void Start()
    {
        originalspeed = speed;
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
        coll = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>(); // AudioSource ������Ʈ ��������
    }
    void FixedUpdate()
    {
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
        DetectPlayerAndAttack();
    }
    void DetectPlayerAndAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                anim.SetTrigger("Attack");
            }
        }
    }
    void LateUpdate()
    {
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;
        spriter.flipX = target.position.x < rigid.position.x;
    }
    void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Death", false);
        health = maxHealth;
    }
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
        deathAudioClip = data.audioClip; // ���� �� ����� AudioClip ����
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")) return;
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.health -= 10f;
        }
        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());
        StartCoroutine(Alphablink());
        if (health > 0)
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Death", true);
            DropItem();
            PlayDeathAudio(); // ���� �� ����� ���
        }
    }
    void PlayDeathAudio()
    {
        if (deathAudioClip != null)
        {
            audioSource.clip = deathAudioClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("deathAudioClip is null, cannot play death audio.");
        }
    }
    IEnumerator KnockBack()
    {
        yield return wait;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }
    IEnumerator Alphablink()
    {
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(1, 1, 1, 1);
    }
    void DropItem()
    {
        Instantiate(itemPrefab, transform.position, Quaternion.identity);
    }
    void Dead()
    {
        gameObject.SetActive(false);
    }
}

