using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlayer : MonoBehaviour
{
    public Text nameText;
    public Text storyText;
    public int buttonIndex = -1;
    public string[] nameTexts = new string[]
    {
        "수도승",
        "의사",
        "마법사",
        " "
    };
    public string[] storyTexts = new string[]
    {
        "오래 전, 높은 산의 수도원에서 베네딕토 수도승은 기도와 명상을 통해 신비한 힘을 얻었습니다. 어느 날, 그는 꿈에서 세계의 균형을 유지하는 보석을 발견했고, 이것을 찾아야 세계가 평화로울 것이라는 예언을 들었습니다.",
        "의사 스토리",
        "마법사 스토리",
        " "
    };
   


    public void ChangeText(int buttonIndex)
    {
        nameText.text = nameTexts[buttonIndex];
        storyText.text = storyTexts[buttonIndex];
    }


}
