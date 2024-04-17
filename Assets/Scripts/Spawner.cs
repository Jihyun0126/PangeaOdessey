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
        //자기 자신도 포함
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        //자기 자신의 우변을 더함
        timer += Time.deltaTime;
        //시간에 맞춰 레벨을 올림
        level = Mathf.FloorToInt(GameManager.instance.gameTime / 10f); //0레벨 
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
        //자기 자신을 빼기 위해 0부터가 아닌 1부터
        enemy.transform.position = spawnPoint[Random.Range(1,spawnPoint.Length)].position;
    }
}
