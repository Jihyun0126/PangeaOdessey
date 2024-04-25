using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 15 * 60f;
    [Header("# Player info")]
    public int health;
    public int maxHealth = 100;
    public int kill;
    [Header(" Game Object")]
    public PoolManager pool;
    public Player player;

    void Awake()
    {
        instance = this;
        health = maxHealth;
    }
    void Update()
    {
        gameTime += Time.deltaTime;
        if(gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
}
