using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;

    public int count;
    public float speed;


    float timer;
    Player player;

    void Awake()
    {
        player = GetComponentInParent<Player>();
    }


    void Start()
    {
        Init();
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            case 1:
                timer += Time.deltaTime;

                if(timer> speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
            default:
                break;
        }
    }
    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150;
                BatchShield();
                break;
            case 1:
                speed = 0.3f;
                break;
            default:
                break;
        }
    }
    void BatchShield() //방패 배치하는 함수
    {
        for (int index = 0; index < count; index++)
        {
            Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
            bullet.parent = transform;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 2f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1); // -1은 계속 관통
        }
    }

    void BatchSword() //검 배치하는 함수
    {
        Transform sword = GameManager.instance.pool.Get(prefabId).transform;
        sword.parent = transform;
    }
    void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;

    }
}