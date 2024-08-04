using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionUpdater : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public Transform target; // Referencia al objeto cuyo `y` se actualizará

    public float heightOffset; // Desplazamiento en la altura

    void Update()
    {
        if (player != null && target != null)
        {
            // Obtiene la posición x del jugador
            float playerX = player.position.x;

            // Actualiza la posición del objetivo con la altura deseada
            target.position = new Vector3(playerX, player.position.y + heightOffset, target.position.z);
        }
    }
}
