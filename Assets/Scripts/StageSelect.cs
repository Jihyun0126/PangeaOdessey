using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    public Text stageName;
    public int buttonIndex = -1;

    public void ChangeStageName(int buttonIndex)
    {
        stageName.text = stageNames[buttonIndex];
    }

    public string[] stageNames = new string[]
    {
        "",
        "고요한 설원", 
        "설산의 그림자",
        "아몬의 골짜기",
        "들판",
        "거대한 숲",
        "더 거대한 숲",
        "사막 입구",
        "출구 없는 사막",
        "하마다",
        "어둠1",
        "어둠2",
        "어둠3"
    };
}
