using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   public static  UIManager Instance = null;
   public bool[] panelStates;

   
   void Awake() {
      if(Instance == null){
        Instance = this;
      }  else if (Instance != this){
        Destroy(this.gameObject);
      }
      DontDestroyOnLoad(this.gameObject);
   }
   public void SavePanelStates(bool[] states){
    panelStates = states;
   }

}
