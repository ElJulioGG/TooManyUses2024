using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowY : MonoBehaviour
{
    public Transform player;  // Asigna el Transform del jugador en el inspector
    public float heightMultiplier = 10.0f;  // Factor multiplicador para volar más alto
    private float initialY;  // Posición inicial en y del objeto

    void Start()
    {
        // Guardar la posición inicial en y del objeto
        initialY = transform.position.y;
    }

    void Update()
    {
        // Obtener la posición actual del objeto
        Vector3 currentPosition = transform.position;

        // Obtener la posición en y del jugador, asegurarse de que sea positiva y multiplicarla para volar más alto
        float playerY = Mathf.Abs(player.position.y) * heightMultiplier;

        // Ajustar la posición en y del objeto sin afectar su posición inicial
        currentPosition.y = initialY + playerY;

        // Asignar la nueva posición al objeto
        transform.position = currentPosition;
    }
}