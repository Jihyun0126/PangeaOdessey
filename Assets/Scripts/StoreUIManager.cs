using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUIManager : MonoBehaviour
{
    public Text StoreGrade;
    public Text GoldText;
    public int Gold;
    Color textcolor;
    Button upGradeButton;
    Button rerollButton;
    // Start is called before the first frame update
    void Start(){
        upGradeButton = GameObject.Find("ShopUpgrade").GetComponent<Button>();
        rerollButton = GameObject.Find("RerollButton").GetComponent<Button>();
    }
    void Update(){
        GoldText.text = Gold.ToString();
        if(Gold < 1500){
            upGradeButton.interactable =false;
        }else{
            upGradeButton.interactable =true;
        }
        if(Gold < 1500){
            rerollButton.interactable =false;
        }else{
            rerollButton.interactable =true;
        }
    }

    public void UpgradeText()
    {
        if(Gold >= 1500){
            textcolor = new Color32(0,0,255,255);
            StoreGrade.text = "2단계 상점";
            StoreGrade.color = textcolor;
            Gold += -1500;
        }
    }

    public void Reroll(){
        Gold += -50;
        GoldText.text = Gold.ToString();
    }
    
}
