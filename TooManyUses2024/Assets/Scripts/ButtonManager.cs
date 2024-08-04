using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void goMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        print("goMainMenu");
    }
    public void goGameplay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        print("goGameplay");
    }
    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        print("RealoadScene");
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.R))
        {
            reloadScene();
        }
    }
}
