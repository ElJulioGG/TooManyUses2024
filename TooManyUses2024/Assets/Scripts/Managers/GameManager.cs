using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //public variables
    public bool playerCanMove = true;
    public bool playerCanAtack = true;
    public bool playerIsInvincible = false;
    public bool playerHasBeenHit = false;

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
}
