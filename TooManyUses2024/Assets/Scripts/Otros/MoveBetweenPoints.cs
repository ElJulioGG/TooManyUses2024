using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenPoints : MonoBehaviour
{
    public GameObject[] points; // Array de puntos a los que se moverá el objeto.
    public float[] speeds; // Velocidades para cada punto.

    private int currentPointIndex = 0; // Índice del punto actual.

    void Update()
    {
        if (points.Length == 0 || speeds.Length == 0) return; // Verifica que hay puntos y velocidades asignadas.

        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        // Mover el objeto hacia el punto actual.
        transform.position = Vector3.MoveTowards(transform.position, points[currentPointIndex].transform.position, speeds[currentPointIndex] * Time.deltaTime);

        // Si el objeto ha llegado al punto actual, cambiar al siguiente punto.
        if (Vector3.Distance(transform.position, points[currentPointIndex].transform.position) < 0.1f)
        {
            currentPointIndex = (currentPointIndex + 1) % points.Length; // Cambia al siguiente punto o vuelve al primero si ya pasó por todos.
        }
    }
}