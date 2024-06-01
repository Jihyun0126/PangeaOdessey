using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float damage;
    private CircleCollider2D circleCollider;

    Rigidbody2D rigid;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;
        circleCollider.radius = 2f;
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, 3f);
        foreach(Collider2D collider2D in collider2Ds)
        {
            if (collider2D.tag != "bullet") { 
                Debug.Log(collider2D.tag);
            }
        }
        this.gameObject.SetActive(false);
        circleCollider.radius = 0.5f;
    }
    public void Init(float damage, Vector3 dir)
    {
        this.damage = damage;
        rigid.velocity = dir * 15f;
    }
}
