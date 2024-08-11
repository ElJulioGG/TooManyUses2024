using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpwards : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento hacia arriba.

    void Update()
    {
        MoveUp();
    }

    void MoveUp()
    {
        // Mueve el objeto hacia arriba a la velocidad especificada.
        transform.position += Vector3.up * speed * Time.deltaTime;
    }
}
