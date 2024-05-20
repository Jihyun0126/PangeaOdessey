using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SelectWeapon : MonoBehaviour
{
    public Sprite[] weaponImages; 
    //태그를 randomnumber리스트에서 사용할 순서로 사용
    Dictionary<string, int> tagToIndex = new Dictionary<string, int>();

    void Start()
    {
        //이후에 서버에서 가져오는 방식으로 변경 예정
        weaponImages = new Sprite[6];
        weaponImages[0] = Resources.Load<Sprite>("1");
        weaponImages[1] = Resources.Load<Sprite>("2");
        weaponImages[2] = Resources.Load<Sprite>("3");
        weaponImages[3] = Resources.Load<Sprite>("4");
        weaponImages[4] = Resources.Load<Sprite>("5");
        weaponImages[5] = Resources.Load<Sprite>("6");

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

        // 태그를 확인하고 태그에 해당하는 숫자를 weaopImages배열의 인덱스로 사용
        // 중복없는 숫자를 인덱스로 사용해서 중복된 이미지가 뜨지 않도록함
        if(tagToIndex.ContainsKey(objectTag)){
            int index = tagToIndex[objectTag];
            Debug.Log(index);

            if (imageComponent != null && weaponImages[randomScript.randomNumber[index]] != null)
            {
                imageComponent.sprite = weaponImages[randomScript.randomNumber[index]];
            }
            else
            {
                Debug.LogError("Image component or new sprite is not set.");
            }
        }
        
    }

}
