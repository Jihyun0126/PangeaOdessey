using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public WeaponData[] weaponDatas;
    public int weaponId;
    
    List<WeaponData> inventory;
    const int inventorySize = 5;

    public Image[] itemSlots;
    public GameObject[] weaponObjects;

    void Start(){
        this.weaponId = 0; 
        inventory = new List<WeaponData>(inventorySize);
    }
    void Update(){
        
    }

    public void BuyWeapon(){
        if(weaponId > 0){
            if(inventory.Count >= inventorySize){
                Debug.Log("인벤토리가 가득 찼습니다.");
                return;
            }

            int id = weaponId;
            foreach(var weapon in weaponDatas){
                if(id == weapon.id){
                    inventory.Add(weapon);
                    Debug.Log(weapon.name);
                    UpdateSlot();
                }
            } 
        }else{
            Debug.Log("선택된 무기가 없습니다.");
        }
        
    }
    // 슬롯에 이미지 업데이트
    void UpdateSlot(){
        for(int i = 0; i < itemSlots.Length; i++){
            if(i < inventory.Count){
                itemSlots[i].sprite = inventory[i].image;
                itemSlots[i].enabled = true;
                ActivateWeapon(inventory[i].id); 
            }else{
                itemSlots[i].sprite = null;
                itemSlots[i].enabled = false;
            }
            
        }
    }

    void ActivateWeapon(int id){
        foreach(var weaponObject in weaponObjects){
            Weapon weaponScript = weaponObject.GetComponent<Weapon>();
            if(weaponScript != null && weaponScript.prefabId == id){
                weaponObject.SetActive(true);
            }else{
                weaponObject.SetActive(false);
            }
        }
    }

   
   
  
}
