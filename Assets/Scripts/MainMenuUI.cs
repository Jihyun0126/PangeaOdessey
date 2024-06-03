using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void OnClickGameStartButton()
    {
        Debug.Log("Game Start~");
    }
    public void ReStartButton(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
