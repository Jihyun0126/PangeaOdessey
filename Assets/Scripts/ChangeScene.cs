using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void EndGame()
    {
        SceneManager.LoadScene("MainScene");
    }

}
