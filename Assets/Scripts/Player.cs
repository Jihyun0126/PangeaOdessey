using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public float curTime;
    public float coolTime;
    public Scanner scanner;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        curTime = 0;
    }
    // Update is called once per frame
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
        if (curTime <= 0)
        {
            anim.SetTrigger("atk");
            curTime = coolTime; 

        }
        else
        {
            curTime -= Time.deltaTime; 
        }
    }

    void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void LateUpdate()
    {
        anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.health -= Time.deltaTime * 10;
        }
        if (!GameManager.instance.isLive) return;
    }
    public void TakeDamage(float amount)
    {
        GameManager.instance.health -= amount;
        if (GameManager.instance.health < 0) GameManager.instance.health = 0;
        Debug.Log("Health after damage: " + GameManager.instance.health);

        // �ʿ� �� �÷��̾ �׾��� �� ���� �߰�
        if (GameManager.instance.health <= 0)
        {
            anim.SetTrigger("Dead");
        }
    }
}
