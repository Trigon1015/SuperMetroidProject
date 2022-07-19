using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        
    }
    public AudioSource mainMenuMusic, levelMusic, bossMusic, levelMusic2;
    public AudioSource[] sfx;

    public void PlayMainMenuMusic()
    {
       
        levelMusic.Stop();
        levelMusic2.Stop();
        bossMusic.Stop();
        mainMenuMusic.Play();
    }

    public void PlayLevelMusic()
    {
        if(!levelMusic.isPlaying && !levelMusic2.isPlaying)
        {
            bossMusic.Stop();
            mainMenuMusic.Stop();
            
            levelMusic.Play();
        }
        
    }

    public void PlayLevelMusic2()
    {
        if (!levelMusic2.isPlaying)
        {
            bossMusic.Stop();
            mainMenuMusic.Stop();
            levelMusic.Stop();
            levelMusic2.Play();
        }

    }

    public void PlayBossMusic()
    {
        levelMusic.Stop();
        levelMusic2.Stop();
        bossMusic.Play();
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }

    public void PlaySFXAdjusted(int sfxToAdjust)
    {
        sfx[sfxToAdjust].pitch = Random.Range(0.8f, 1.2f);
        PlaySFX(sfxToAdjust);
    }
}
