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
    public Text inventoryInfo;
    public int slotNumber;
    int selectInventory;

    GameObject obj;

    void Start(){
        this.weaponId = 0; 
        inventory = new List<WeaponData>(inventorySize);
    }
    void Update(){
        
    }

    public void BuyWeapon(){
        //인벤토리가 비었는지 확인
        if(weaponId > 0){
            if(inventory.Count >= inventorySize){
                Debug.Log("인벤토리가 가득 찼습니다.");
                return;
            }

            int id = weaponId;
            foreach(var weapon in weaponDatas){
                if(id == weapon.id){
                    // 중복되는 무기종류가 있는지 확인
                    foreach(var item in inventory){
                        if(item.category == weapon.category){
                            Debug.Log("중복되는 종류의 무기가 있습니다.");
                            return;
                        }
                    }
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

        //판매할 인벤토리 인덱스 slotNumber

        if(inventory.Count == 0){
            Debug.Log("인벤토리에 아무 무기도 없습니다.");
            return;
        }
        int soldWeaponId = inventory[slotNumber].id;
        
        GameManager.bitCoin += inventory[slotNumber].price / 2;

        // 판매된 무기의 ID를 가져와 해당 무기를 비활성화합니다.
        UnActivateWeapon(soldWeaponId);

        // 인벤토리의 무기들을 한 칸씩 앞으로 땡깁니다.
        for(int i = slotNumber + 1; i < inventory.Count; i++){
            inventory[i - 1] = inventory[i];
        }
        // 마지막 칸은 무기를 판매하여 비어있으므로, 마지막 무기를 삭제합니다.
        
        inventory.RemoveAt(inventory.Count - 1);

        // 업데이트된 인벤토리를 반영하여 슬롯을 업데이트합니다.
        Debug.Log("현재 아이템 수 : " + inventory.Count);
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
                Debug.Log("무기 활성화 실패");
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
    public void SetInventoryInfo(){
        selectInventory = slotNumber;
        Debug.Log("선택한 인벤토리 인덱스: " + selectInventory);
        
        // slotNumber가 유효한 인덱스 범위 내에 있는지 확인합니다.
        if (selectInventory >= 0 && selectInventory < inventory.Count) {
            // inventory 배열의 해당 인덱스가 null이 아닌지 확인합니다.
            inventoryInfo.text = inventory[selectInventory].name +"\n"
                + "판매가격 : " + inventory[selectInventory].price/2;

            
        } else {
            inventoryInfo.text = "";
        }       
        
    }


    // 인벤토리 인덱스 전달
    public void SlotNum(){
        int s = slotNumber;
        obj = GameObject.Find("BuyButton");
        obj.GetComponent<WeaponManager>().slotNumber = s;
    }
        
    

   
   
  
}
