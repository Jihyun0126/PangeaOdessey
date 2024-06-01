using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundsManager : MonoBehaviour
{   
    public AudioMixer mixer;

    public void SetLevel(float volume){
        mixer.SetFloat("BGM", Mathf.Log10(volume)*20);
    }
}
