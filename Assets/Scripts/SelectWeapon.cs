using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Data.Common;

public class SelectWeapon : MonoBehaviour
{
    public Sprite[] weaponImages;
    public WeaponData[] weaponDatas;
    public int HolderIndex;
    public int SelectId;    

    GameObject obj;
    public Text weaponName;
    public Text weaponInfo;

    void Start()
    {
        weaponImages = new Sprite[6];
        int minLength = Mathf.Min(weaponImages.Length, weaponDatas.Length);
        for (int i = 0; i < minLength; i++)
        {
            weaponImages[i] = weaponDatas[i].image;
        }

    }

    public void Reroll()
    {
        // MakeRandomNumber에서 만든 리스트 가져옴
        MakeRandomNumber randomScript = FindAnyObjectByType<MakeRandomNumber>();
        Image imageComponent = GetComponent<Image>();
        SelectId = 0;
        int id = randomScript.randomNumber[HolderIndex];
        Debug.Log(id);

        // id에 해당하는 이미지와 id 저장
        foreach(WeaponData weapon in weaponDatas){
            if(weapon.id == id){
                SelectId = id;
                imageComponent.sprite = weapon.image;
            }
        }     
    }

    public void SetWeaponInfo(){
        obj = GameObject.Find("BuyButton");
        obj.GetComponent<WeaponManager>().weaponId = SelectId;
        foreach(var data in weaponDatas){
            if(data.id == SelectId){
                weaponName.text = data.name;
                weaponInfo.text = "데미지 : " + data.damage + "\n" +
                    "속성 : "+ data.property;
            }
        }
    }

}
