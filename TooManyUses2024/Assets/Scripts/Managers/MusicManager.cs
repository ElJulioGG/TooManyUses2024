using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicManager : MonoBehaviour
{
    public UnityEvent unityEvent;
    // Start is called before the first frame update
    void Start()
    {
        unityEvent.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playMainMusic()
    {
        AudioManager.instance.musicSource.Stop();
        AudioManager.instance.PlayMusic("MainMusic");
    }
    public void playMenuMusic()
    {
        AudioManager.instance.musicSource.Stop();
        AudioManager.instance.PlayMusic("MenuMusic");
    }
}
