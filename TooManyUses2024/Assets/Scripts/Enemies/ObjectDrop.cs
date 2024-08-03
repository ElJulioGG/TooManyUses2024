using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrop : MonoBehaviour
{
    public float velocidadCaida = 5f;

    void Update()
    {
        // Movimiento hacia abajo
        transform.Translate(Vector2.down * velocidadCaida * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Acci�n al colisionar, puedes a�adir efectos o eliminar el huevo
        if (col.gameObject.CompareTag("Player"))
        {
            // L�gica para da�o al jugador
        }

        // Destruir el huevo al colisionar con cualquier objeto
        Destroy(gameObject,3f);
    }
}