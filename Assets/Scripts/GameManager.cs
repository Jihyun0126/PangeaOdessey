using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    [Header("# Player info")]
    public float health = 100f; // int -> float
    public float maxHealth = 100f; // int -> float
    public int kill;
    [Header(" Game Object")]
    public PoolManager pool;
    public Player player;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime) {
            
        }
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health < 0) health = 0;
        Debug.Log("Health after damage: " + health);
    }
}
