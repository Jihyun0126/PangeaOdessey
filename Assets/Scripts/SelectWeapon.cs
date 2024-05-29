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
    public int SelectId;
    //태그를 randomnumber리스트에서 사용할 순서로 사용
    Dictionary<string, int> tagToIndex = new Dictionary<string, int>();

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
       /*weaponImages[0] = weaponDatas[0].image;
        weaponImages[1] = weaponDatas[1].image;
        weaponImages[2] = weaponDatas[2].image;
        weaponImages[3] = weaponDatas[3].image;
        weaponImages[4] = weaponDatas[4].image;
        weaponImages[5] = weaponDatas[5].image;*/

        tagToIndex["Shop1"] = 0;
        tagToIndex["Shop2"] = 1;
        tagToIndex["Shop3"] = 2;
        tagToIndex["Shop4"] = 3;

    }

    public void Reroll()
    {
        // MakeRandomNumber에서 만든 리스트 가져옴
        MakeRandomNumber randomScript = FindAnyObjectByType<MakeRandomNumber>();

        string objectTag = gameObject.tag;
        Image imageComponent = GetComponent<Image>();
        SelectId = 0;
        // 태그를 확인하고 태그에 해당하는 숫자를 weaopImages배열의 인덱스로 사용
        // 중복없는 숫자를 인덱스로 사용해서 중복된 이미지가 뜨지 않도록함
        if(tagToIndex.ContainsKey(objectTag)){
            int index = tagToIndex[objectTag];
            Debug.Log(index);

            if (imageComponent != null && weaponImages[randomScript.randomNumber[index]] != null)
            {
                imageComponent.sprite = weaponImages[randomScript.randomNumber[index]];
                SelectId = weaponDatas[randomScript.randomNumber[index]].id;
            }
            else
            {
                Debug.LogError("Image component or new sprite is not set.");
            }
        }
        
    }

    public void SetWeaponInfo(){
        foreach(var data in weaponDatas){
            if(data.id == SelectId){
                
                weaponName.text = data.name;
                weaponInfo.text = "데미지 : " + data.damage + "\n" +
                    "속성 : "+ data.property;
            }
        }
    }

}
