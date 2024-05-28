using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    public Image overlayImage;
    public Text overlayText;
    public string warningMessage = "곧 어둠이 생성됩니다"; // 경고 문구 내용
    public float fadeDuration = 2.0f; // 페이드 인/아웃에 걸리는 시간
    public float visibleDuration = 5.0f; // 화면이 완전히 어두워진 상태에서 유지되는 시간
    public float warningDuration = 5.0f; // 경고 문구가 표시되는 시간
    private float elapsedTime = 0f;
    private bool isFadingIn = true;
    private bool isHolding = false;

    void Start()
    {
        // 초기 불투명도를 0으로 설정 (완전 투명)
        overlayImage.color = new Color(0, 0, 0, 0);
        overlayText.color = new Color(1, 1, 1, 0);
        overlayText.text = warningMessage;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (isFadingIn)
        {
            if (elapsedTime > warningDuration)
            {
                float alpha = Mathf.Clamp01((elapsedTime - warningDuration) / fadeDuration);
                overlayImage.color = new Color(0, 0, 0, alpha);
                overlayText.color = new Color(1, 1, 1, 0); // 어두워질 때는 텍스트를 보이지 않게

                if (elapsedTime > warningDuration + fadeDuration)
                {
                    isFadingIn = false;
                    isHolding = true;
                    elapsedTime = 0f;
                }
            }
            else
            {
                // 경고 문구를 표시
                overlayText.color = new Color(1, 1, 1, 1);
            }
        }
        else if (isHolding)
        {
            if (elapsedTime > visibleDuration)
            {
                isHolding = false;
                elapsedTime = 0f;
            }
        }
        else
        {
            float alpha = 1.0f - Mathf.Clamp01(elapsedTime / fadeDuration);
            overlayImage.color = new Color(0, 0, 0, alpha);
            overlayText.color = new Color(1, 1, 1, 0); // 밝아질 때는 텍스트를 보이지 않게

            if (elapsedTime > fadeDuration)
            {
                isFadingIn = true;
                elapsedTime = 0f;
                overlayText.color = new Color(1, 1, 1, 1); // 다시 어두워지기 전 경고 문구를 보이게
            }
        }
    }
}
