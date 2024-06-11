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

    public void MainScene()
    {
        SceneManager.LoadScene("1MainScene");
    }

    public void ChooseCharacterScene(){
        SceneManager.LoadScene("2ChooseCharacterScene");
    }

    public void MapScene(){
        SceneManager.LoadScene("3MapScene");
    }

    public void Grass(){
        SceneManager.LoadScene("Grass");
    }
    public void GrassStage(){
        SceneManager.LoadScene("GrassStage");
    }

    public void Ice(){
        SceneManager.LoadScene("Ice");
    }
    public void IceStage(){
        SceneManager.LoadScene("IceStage");
    }

    public void Fire(){
        SceneManager.LoadScene("Fire");
    }
    public void FireStage(){
        SceneManager.LoadScene("FireStage");
    }

    public void Dark(){
        SceneManager.LoadScene("Dark");
    }
    public void DarkStage(){
        SceneManager.LoadScene("DarkStage");
    }
    
}
