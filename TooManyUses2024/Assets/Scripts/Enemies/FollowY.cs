using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowY : MonoBehaviour
{
    public Transform player;  // Asigna el Transform del jugador en el inspector
    public float heightMultiplier = 10.0f;  // Factor multiplicador para volar m�s alto
    private float initialY;  // Posici�n inicial en y del objeto

    void Start()
    {
        // Guardar la posici�n inicial en y del objeto
        initialY = transform.position.y;
    }

    void Update()
    {
        // Obtener la posici�n actual del objeto
        Vector3 currentPosition = transform.position;

        // Obtener la posici�n en y del jugador, asegurarse de que sea positiva y multiplicarla para volar m�s alto
        float playerY = Mathf.Abs(player.position.y) * heightMultiplier;

        // Ajustar la posici�n en y del objeto sin afectar su posici�n inicial
        currentPosition.y = initialY + playerY;

        // Asignar la nueva posici�n al objeto
        transform.position = currentPosition;
    }
}