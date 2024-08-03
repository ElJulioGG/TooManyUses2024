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
        // Acción al colisionar, puedes añadir efectos o eliminar el huevo
        if (col.gameObject.CompareTag("Player"))
        {
            // Lógica para daño al jugador
        }

        // Destruir el huevo al colisionar con cualquier objeto
        Destroy(gameObject,3f);
    }
}