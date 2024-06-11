using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.bitCoin++;
            Debug.Log(GameManager.bitCoin);
            gameObject.SetActive(false);
        }
    }
}
