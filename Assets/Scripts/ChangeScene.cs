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

    public void GrassScene(){
        SceneManager.LoadScene("Grass");
    }
    public void GrassBossScene(){
        SceneManager.LoadScene("GrassBoss");
    }

    public void IceScene(){
        SceneManager.LoadScene("Ice");
    }
    public void IceBossScene(){
        SceneManager.LoadScene("IceBoss");
    }

    public void FireScene(){
        SceneManager.LoadScene("Fire");
    }
    public void FireBossScene(){
        SceneManager.LoadScene("FireBoss");
    }

    public void DarkScene(){
        SceneManager.LoadScene("Dark");
    }
    public void DarkBossScene(){
        SceneManager.LoadScene("DarkBoss");
    }
    

}
