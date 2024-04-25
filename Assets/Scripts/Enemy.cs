using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;
    public Animator anim;
    public LayerMask playerLayer; // �÷��̾� ���̾� ����ũ
    public float detectionRadius = 5f; // ���� ���� ����

    bool isLive;
    Rigidbody2D rigid;
    SpriteRenderer spriter;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!isLive) return;

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
        if (!isLive) return;
        spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable()
    {
        // �÷��̾� ������Ʈ�� ã�Ƽ� target�� �Ҵ�
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")) return;
        health -= collision.GetComponent<Bullet>().damage;
        if(health > 0)
        {

        }
        else
        {
            Dead();
        }
    }
    void Dead()
    {
        gameObject.SetActive(false);
    }
}

