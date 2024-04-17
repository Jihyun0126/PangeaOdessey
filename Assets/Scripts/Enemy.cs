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
    public LayerMask playerLayer; // 플레이어 레이어 마스크
    public float detectionRadius = 5f; // 공격 감지 범위

    bool isLive;
    Rigidbody2D rigid;
    SpriteRenderer spriter;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                // 플레이어를 감지했을 때의 동작
                // 여기에 플레이어를 공격하는 로직을 추가하세요.
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
        // 플레이어 오브젝트를 찾아서 target에 할당
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
        //총알인지 아닌지 확인
        if (!collision.CompareTag("Bullet")) return;
        //체력에 데미지를 빼줌
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

