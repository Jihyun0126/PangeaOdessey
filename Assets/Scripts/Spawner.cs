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
        //자기 자신도 포함
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        //자기 자신의 우변을 더함
        timer += Time.deltaTime;
        //시간에 맞춰 레벨을 올림
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f),spawnData.Length - 1); //0레벨 
        if(timer > spawnData[level].spawnTime)
        {
            Spawn();
            timer = 0f;
        }
    }
    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        //자기 자신을 빼기 위해 0부터가 아닌 1부터
        Debug.Log("Enemy Spawned: " + enemy.name); // Debug Log 추가
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
    public AudioClip audioClip; // AudioClip 필드 추가
}