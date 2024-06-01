using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;
    private float time;

    void Start()
    {
        StartCoroutine(LoadSceneProcess()); // 코루틴 실행
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    public IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);  // 다음 씬을 로드.
        op.allowSceneActivation = false;  // 씬 활성화를 허용하지 않는다.
        
        while (!op.isDone)
        {
            yield return new WaitForSeconds(3.0f); // 3초 대기
            
            op.allowSceneActivation = true;            
            yield return null;
        }
    }
}
