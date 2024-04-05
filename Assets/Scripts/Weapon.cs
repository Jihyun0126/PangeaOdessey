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

    void Update()
    {
        
    }
    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = -150;
                Batch();
                break;
            default:
                break;
        }
    }
    void Batch() //방패 배치하는 함수
    {
        for(int index=0; index<count; index++)
        {
            Transform bullet =GameManager.instance.pool.Get(prefabId).transform;
            bullet.parent = transform;
            bullet.GetComponent<Bullet>().Init(damage, -1); // -1은 계속 관통
        }
    }
}
