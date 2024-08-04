using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
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
        // if escena is "menu" then play(MenuMusic :p)
        //else
       // setMusic("Aftermath");
    }
    public void setMusic(string name)
    {
        AudioManager.instance.musicSource.Stop();
        AudioManager.instance.PlayMusic(name);
        print("Music Change");
    }
    
    public void musicDelayTransition(string name, float delay)
    {
        StartCoroutine(setMusicDelay(name, delay));
    }
    IEnumerator setMusicDelay(string name, float delay)
    {
        
        yield return new WaitForSeconds(delay);
        AudioManager.instance.musicSource.Stop();
        AudioManager.instance.PlayMusic(name);
        
    }
}
