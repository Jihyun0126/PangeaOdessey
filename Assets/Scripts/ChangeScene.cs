using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{   
    
    public void StartGame()
    {
        LoadingSceneManager.LoadScene("GameScene");
    }

    public void StartGameScene()
    {
        LoadingSceneManager.LoadScene("GameScene");
    }

    public void EndGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    

}
