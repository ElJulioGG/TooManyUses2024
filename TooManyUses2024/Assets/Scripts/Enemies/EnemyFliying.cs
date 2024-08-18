using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFliying : MonoBehaviour
{
    public float velocidad; // Velocidad de movimiento del enemigo
    public bool moverseALaDerecha = false; // Si es true, se mueve a la derecha; si es false, se mueve a la izquierda

    void Start()
    {
    }

    void Update()
    {
        Mover();
        //Disparar();
    }

    void Mover()
    {
        // Movimiento hacia la derecha o izquierda según la variable moverseALaDerecha
        Vector3 direccion = moverseALaDerecha ? Vector3.right : Vector3.left;
        transform.Translate(direccion * velocidad * Time.deltaTime);
    }

    void Disparar()
    {
        // Implementación del disparo aquí
    }
}
