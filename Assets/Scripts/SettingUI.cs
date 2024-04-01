using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    // 추후 사운드 스크롤 2개 추가예정

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Setting UI 닫고 비활성화 해주는 함수
    // Coroutine 사용
    public void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }

    private IEnumerator CloseAfterDelay()
    {
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        animator.ResetTrigger("Close");
    }
}
