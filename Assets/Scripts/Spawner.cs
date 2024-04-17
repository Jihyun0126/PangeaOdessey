using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    float timer;
    int level;
    void Awake()
    {
        //�ڱ� �ڽŵ� ����
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        //�ڱ� �ڽ��� �캯�� ����
        timer += Time.deltaTime;
        //�ð��� ���� ������ �ø�
        level = Mathf.FloorToInt(GameManager.instance.gameTime / 10f); //0���� 
        Debug.Log(GameManager.instance.gameTime);
        //Debug.Log(level);
        if(timer > (level == 0 ? 0.5f : 0.2f))
        {
            Spawn();
            timer = 0f;
        }
    }
    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(level);
        //�ڱ� �ڽ��� ���� ���� 0���Ͱ� �ƴ� 1����
        enemy.transform.position = spawnPoint[Random.Range(1,spawnPoint.Length)].position;
    }
}
