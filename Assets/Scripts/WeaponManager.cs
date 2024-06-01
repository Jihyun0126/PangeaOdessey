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
                    if(GameManager.bitCoin >= weapon.price){
                        GameManager.bitCoin -= weapon.price;
                    }else{
                        return;
                    }
                    inventory.Add(weapon);   
                    Debug.Log(weapon.name);
                    UpdateSlot();
                }
            } 
        }else{
            Debug.Log("선택된 무기가 없습니다.");
        }
        
    }

    public void SellWeapon(){
        Debug.Log(inventory.Count);
        int n = inventory.Count;
        if(inventory.Count == 0){
            Debug.Log("인벤토리에 아무 무기도 없습니다.");
            return;
        }
        int soldWeaponId = inventory[0].id;
        // 판매할 무기를 제거하고 해당 슬롯을 비활성화합니다.
        inventory.RemoveAt(0);
        itemSlots[0].sprite = null;
        itemSlots[0].enabled = false;

        // 판매된 무기의 ID를 가져와 해당 무기를 비활성화합니다.
        UnActivateWeapon(soldWeaponId);
        UpdateSlot();

        // 인벤토리의 무기들을 한 칸씩 앞으로 땡깁니다.
        for(int i = 1; i < inventory.Count; i++){
            inventory[i - 1] = inventory[i];
            
        } Debug.Log(inventory.Count);
        // 마지막 칸은 무기를 판매하여 비어있으므로, 마지막 무기를 삭제합니다.
        
        //inventory.RemoveAt(inventory.Count - 1);

        // 업데이트된 인벤토리를 반영하여 슬롯을 업데이트합니다.
        UpdateSlot();
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
    // 무기 활성화
    void ActivateWeapon(int id){
        foreach(var weaponObject in weaponObjects){
            Weapon weaponScript = weaponObject.GetComponent<Weapon>();
            if(weaponScript != null && weaponScript.prefabId == id){
                weaponObject.SetActive(true);
            }else{
                
            }
        }
    }

    void UnActivateWeapon(int id){
        foreach(var weaponObject in weaponObjects){
            Weapon weaponScript = weaponObject.GetComponent<Weapon>();
            if(weaponScript != null && weaponScript.prefabId == id){
                weaponObject.SetActive(false);
            }else{
                
            }
        }
        
    }

   
   
  
}
