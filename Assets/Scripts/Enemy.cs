using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;
    public Animator anim;
    public LayerMask playerLayer; // �÷��̾� ���̾� ����ũ
    public float detectionRadius = 5f; // ���� ���� ����

    bool isLive = true;
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

        // �÷��̾� ���� �� ����
        DetectPlayerAndAttack();
    }

    void DetectPlayerAndAttack()
    {
        // �÷��̾� ����
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                // ���� ��� ���
                anim.SetTrigger("Attack");
                break; // �ϳ��� �÷��̾ �����ϸ� ���� ����
            }
        }
    }

    void LateUpdate()
    {
        spriter.flipX = target.position.x < rigid.position.x;
    }
}
