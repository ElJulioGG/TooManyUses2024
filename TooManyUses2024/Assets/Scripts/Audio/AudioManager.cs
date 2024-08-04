using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sounds[] musicSounds, sfxSounds, FootStepsSounds, Sfx2Sounds, UISounds, Sfx3Sounds, sfxLoop1Sounds, Sfx4Sounds;
    public AudioSource musicSource, sfxSource, FootStepsSource, DoorSource, UISource, sfxSource3, sfxLoopSource1, SfxSource4;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //musica del menu
    }
    public void PlayMusic(string name)
    {
        Sounds s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("sound not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void PlaySfx(string name)
    {
        Sounds s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip); //hay varios metodos para controlar audio
            //ver la documentacion de unity
        }
    }
    public void PlayFootSteps(string name)
    {
        Sounds s = Array.Find(FootStepsSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("sound not found");
        }
        else
        {
            FootStepsSource.PlayOneShot(s.clip);
        }
    }

    public void PlaySfx2(string name)
    {
        Sounds s = Array.Find(Sfx2Sounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("sound not found");
        }
        else
        {
            DoorSource.PlayOneShot(s.clip);
        }
    }
    public void PlaySfx3(string name)
    {
        Sounds s = Array.Find(Sfx3Sounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("sound not found");
        }
        else
        {
            sfxSource3.PlayOneShot(s.clip);
        }
    }
    public void PlaySfx4(string name)
    {
        Sounds s = Array.Find(Sfx4Sounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("sound not found");
        }
        else
        {
            SfxSource4.PlayOneShot(s.clip);
        }
    }
    public void PlaySfxLoop1(string name)
    {
        Sounds s = Array.Find(sfxLoop1Sounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("sound not found");
        }
        else
        {
            sfxLoopSource1.clip = s.clip;
            sfxLoopSource1.Play();
        }
    }
    public void PlayUI(string name)
    {
        Sounds s = Array.Find(UISounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("sound not found");
        }
        else
        {
            UISource.PlayOneShot(s.clip); //hay varios metodos para controlar audio
            //ver la documentacion de unity
        }
    }
}
//s