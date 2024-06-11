using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{   public static Spawner spawn;
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
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
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f),spawnData.Length - 1); //0���� 
        if(timer > spawnData[level].spawnTime)
        {
            Spawn();
            timer = 0f;
        }
    }
    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        //�ڱ� �ڽ��� ���� ���� 0���Ͱ� �ƴ� 1����
        Debug.Log("Enemy Spawned: " + enemy.name); // Debug Log �߰�
        enemy.transform.position = spawnPoint[Random.Range(1,spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
    public AudioClip audioClip; // AudioClip �ʵ� �߰�
}