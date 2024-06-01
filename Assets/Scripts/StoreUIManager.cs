using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUIManager : MonoBehaviour
{
    GameObject Gold;
    public Text StoreGrade;
    public int totalGold;
    Color textcolor;
    Button upGradeButton;
    Button rerollButton;
    int storeGradeId;
    // Start is called before the first frame update
    void Start(){
        totalGold = 100;
        storeGradeId = 1;
        this.Gold = GameObject.FindWithTag("Gold");
        upGradeButton = GameObject.Find("ShopUpgrade").GetComponent<Button>();
        rerollButton = GameObject.Find("RerollButton").GetComponent<Button>();
    }
    void Update(){
        this.Gold.GetComponent<Text>().text = totalGold.ToString();
        if(totalGold < 1500 && storeGradeId == 1){
            upGradeButton.enabled =false;
        }else{
            upGradeButton.enabled =true;
            upGradeButton.interactable =true;
        }
        if(totalGold < 50){
            rerollButton.interactable =false;
        }else{
            rerollButton.interactable =true;
        }
    }

    public void UpgradeText()
    {
        if(totalGold >= 1500){
            textcolor = new Color32(0,0,255,255);
            StoreGrade.text = "2단계 상점";
            StoreGrade.color = textcolor;
            totalGold += -1500;
            storeGradeId = 2;
            upGradeButton.interactable =false;
        }
    }

    public void RerollGold(){
        if(totalGold >= 50)
            totalGold += -50;
    }

    public void UpGold(){
        totalGold += 1000;
    }
}
