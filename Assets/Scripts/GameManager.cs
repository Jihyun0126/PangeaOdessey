using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    [Header("# Player info")]
    public static int bitCoin = 0;
    public float health = 100f; // int -> float
    public float maxHealth = 100f; // int -> float
    public int kill;
    [Header(" Game Object")]
    public PoolManager pool;
    public Player player;
    public Text gold;
    public Text timer;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        health = maxHealth;
        bitCoin = 0;
    }

    void Update()
    {
        gameTime += Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(gameTime);
        string timeString = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        timer.text = timeString;
        if (gameTime > maxGameTime) {
            
        }

        gold.text = bitCoin.ToString()+ "G";
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health < 0) health = 0;
        Debug.Log("Health after damage: " + health);
    }
}
