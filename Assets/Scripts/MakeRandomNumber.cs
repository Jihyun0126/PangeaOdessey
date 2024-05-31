using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MakeRandomNumber : MonoBehaviour
{   
    public List<int> randomNumber;
    public int minNumber , maxNumber;
    StoreUIManager UIManager;

    public void RandomNumberGenerator()
    {
        randomNumber = GetUniqueRandomNumbers(minNumber, maxNumber, 4);
    }

    List<int> GetUniqueRandomNumbers(int min, int max, int count)
    {
        
        // 범위 내의 모든 숫자를 리스트로 만듭니다.
        List<int> allNumbers = Enumerable.Range(min, max - min + 1).ToList();
       
        // 숫자를 랜덤하게 섞는다.
        System.Random random = new System.Random();
        allNumbers = allNumbers.OrderBy(x => random.Next()).ToList();
        List<int> result =allNumbers.Take(count).ToList();
        Debug.Log("Selected numbers : "+ string.Join(", ",result));
        // 원하는 개수만큼 뽑기
        return result;
    }

    public void UpgradeStore(){
        minNumber += 4;
        maxNumber += 5;
    }
    
    
    
}
