using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressStart : MonoBehaviour
{
    public GameObject pressStartText; // El texto de "Press any key to start"


    private void Start()
    {
        GameManager.instance.playerCanMove = false;
        GameManager.instance.playerCanAtack = false;
        // Asegúrate de que los elementos del juego están desactivados al inicio

    }

    private void Update()
    {
        // Detectar cualquier tecla
        if (Input.anyKeyDown && !Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        // Desactivar el texto de "Press any key to start"
        GameManager.instance.playerCanMove = true;
        GameManager.instance.playerCanAtack = true;
        pressStartText.SetActive(false);


    }
}
