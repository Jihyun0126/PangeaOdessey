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
    public LayerMask playerLayer; // 플레이어 레이어 마스크
    public float detectionRadius = 5f; // 공격 감지 범위
    

    bool isLive;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;
    Collider2D coll;

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
    }

    void FixedUpdate()
    {
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        Vector2 dirVec = target.position - rigid.position; 
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
        // 플레이어 감지 및 공격
        //DetectPlayerAndAttack();
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
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;
        spriter.flipX = target.position.x < rigid.position.x;
    }
    void OnEnable()
    {
        // 플레이어 오브젝트를 찾아서 target에 할당
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
        //총알인지 아닌지 확인
        if (!collision.CompareTag("Bullet")) return;
        //체력에 데미지를 빼줌
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
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse); //녹백 크기
        Debug.Log("넉백");
    }
    IEnumerator Alphablink()
    {
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(1, 1, 1, 1);

    }
    // 몬스터가 죽을 때 아이템을 드랍하는 함수
    /*
    void DropItem()
    {
        // 아이템 프리팹을 생성하고 위치를 몬스터 위치로 설정
        GameObject itemGo = Instantiate(itemPrefab, transform.position, Quaternion.identity);
        // 활성화하지 않고 설정
        itemGo.SetActive(false);
        // 몬스터가 죽은 후 아이템을 활성화하도록 설정
        Action onDie = () =>
        {
            itemGo.SetActive(true);
        };
        // 몬스터의 Death 애니메이션이 완료되면 아이템을 활성화
        anim.GetComponent<AnimationEvent>().AddEvent("Death", onDie);
    }*/
    void Dead()
    {
        gameObject.SetActive(false);
    }
}

