using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoader : MonoBehaviour
{
    public AudioManager AM;
    private void Awake()
    {
        if(AudioManager.instance == null)
        {
            AudioManager newAM = Instantiate(AM);
            AudioManager.instance = newAM;
            DontDestroyOnLoad(newAM.gameObject);
        }
    }
}
