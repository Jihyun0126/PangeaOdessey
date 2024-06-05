using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum Infotype { Exp, Level, Kill, Time, Health, BossHP }
    public Infotype type;

    Text myText;
    public Slider mySlider;

    void Awake()
    { 
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();

        if (mySlider == null)
        {
            //Debug.LogError("Slider component not found!");
        }

    }

    void LateUpdate()
    {
        switch (type)
        {
            case Infotype.Exp:
                // Implement experience related UI updates
                break;

            case Infotype.Kill:
                myText.text = string.Format("{0:F0}", GameManager.instance.kill); // GameManager에서 코드 추가해야함
                break;

            case Infotype.Time:
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;

            case Infotype.Health:
                UpdateHealthUI();
                break;

            case Infotype.BossHP:
                UpdateBossHPUI();
                break;
        }
    }

    void UpdateHealthUI()
    {
        float curHealth = GameManager.instance.health;
        float maxHealth = GameManager.instance.maxHealth;
        mySlider.value = curHealth / maxHealth;
        Debug.Log("Slider value: " + mySlider.value);
    }

    void UpdateBossHPUI()
    {
    float curBossHealth = GameManager.instance.bossHealth;
    float maxBossHealth = GameManager.instance.maxBossHealth;
    mySlider.value = curBossHealth / maxBossHealth;
    Debug.Log("Boss HP Slider value: " + mySlider.value);
    }
}
