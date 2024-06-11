using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;
    public float originalspeed;
    public float Wdamage;
    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Dead();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        Wdamage = damage;
        this.damage = damage;
        this.per=per;
        if(per > -1)
        {
            rigid.velocity = dir * 15f;
        }
        if(per == -2)
        {
            rigid.velocity = dir * 15f;
        }
        if(per == -3)
        {
            rigid.velocity = dir * 5f;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1 || per == -2 || per == -3)
            return;
        per--;

        if(per == -1)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
        if (per == -2)
        {
            gameObject.SetActive(false);
        }
        if(per == -3)
        {
            gameObject.SetActive(false);
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        // 현재 트리거된 적 오브젝트를 가져옴
        Enemy enemy = collision.GetComponent<Enemy>();
        enemy.speed = originalspeed;
        if (enemy != null)
        {
            enemy.health -= Wdamage;
            // 적의 속도를 감소시킴
            enemy.speed= 1; // 예시로 0.5배로 감소시킴
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        // 범위를 벗어난 적 오브젝트를 가져옴
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.speed = enemy.originalspeed;
        }
    }

    void Dead()
    {
        Transform target = GameManager.instance.player.transform;
        Vector3 targetPos = target.position;
        float dir = Vector3.Distance(targetPos, transform.position);
        if (dir > 20f)
            this.gameObject.SetActive(false);
    }
}
