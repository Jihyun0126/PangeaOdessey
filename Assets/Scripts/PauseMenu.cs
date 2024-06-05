using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public  static bool GameIsPaused = false;
    public GameObject pauseMenuCanvas;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(GameIsPaused){
                Resume();
            }else{
                Pause();
            }
        }
    }

    public void Resume(){
        
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause(){
        
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
