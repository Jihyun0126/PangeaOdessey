using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectWeapon : MonoBehaviour
{
    public void Reroll()
    {
        // pick random number (id)
        int[] ran = new int[4];
        while(true){
            ran[0] = Random.Range(0, 6);
            ran[1] = Random.Range(0, 6);
            ran[2] = Random.Range(0, 6);
            
            if(ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }
        Debug.Log($"숫자 {ran[0]}  {ran[1]} {ran[2]}");
    }
}
