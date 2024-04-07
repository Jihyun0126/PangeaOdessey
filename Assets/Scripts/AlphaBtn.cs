using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaBtn : MonoBehaviour
{
    // 버튼을 이미지 모양으로 만들어주는 스크립트
    public float AlphaThreshold = 0.4f;

    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;
    }

    
}
