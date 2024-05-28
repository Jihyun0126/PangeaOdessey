using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    SpriteRenderer sr;
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
    WaitForFixedUpdate wait;
    Collider2D coll;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
        coll = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        Vector2 dirVec = target.position - rigid.position; 
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
        // �÷��̾� ���� �� ����
        DetectPlayerAndAttack();
    }

    void DetectPlayerAndAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                // �÷��̾ �������� ���� ����
                // ���⿡ �÷��̾ �����ϴ� ������ �߰��ϼ���.
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
        // �÷��̾� ������Ʈ�� ã�Ƽ� target�� �Ҵ�
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Death",false);
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
        //�Ѿ����� �ƴ��� Ȯ��
        if (!collision.CompareTag("Bullet")) return;
        //ü�¿� �������� ����
        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());
        StartCoroutine(Alphablink());
        if(health > 0)
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Death",true);
            //dropCoin();
        }
    }
    IEnumerator KnockBack()
    {
        yield return wait;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse); //��� ũ��
        Debug.Log("�˹�");
    }
    IEnumerator Alphablink()
    {
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(1, 1, 1, 1);

    }
    /*
    void dropCoin()
    {
        Vector2 dirVec = 
    }*/
    void Dead()
    {
        gameObject.SetActive(false);
    }
}

