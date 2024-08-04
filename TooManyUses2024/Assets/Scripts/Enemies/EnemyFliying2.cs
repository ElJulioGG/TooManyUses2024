using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFliying2 : MonoBehaviour
{
    public float velocidad; // Velocidad de movimiento del enemigo
    public GameObject dropPrefab; // Prefab del huevo a disparar
    public float tiempoEntreDisparos; // Tiempo entre disparos
    private float tiempoSiguienteDisparo;
    public float velocidadDisparo; // Velocidad del disparo

    public Transform puntoA; // Punto A de la patrulla
    public Transform puntoB; // Punto B de la patrulla
    private Transform puntoActual; // Punto al que se dirige el enemigo

    void Start()
    {
        tiempoSiguienteDisparo = Time.time + tiempoEntreDisparos;
        puntoActual = puntoB;
    }

    void Update()
    {
        Mover();
        Disparar();
    }

    void Mover()
    {
        // Movimiento entre puntos A y B
        transform.position = Vector2.MoveTowards(transform.position, puntoActual.position, velocidad * Time.deltaTime);

        // Cambiar de dirección al llegar a un punto
        if (Vector2.Distance(transform.position, puntoActual.position) < 0.1f)
        {
            if (puntoActual == puntoB)
            {
                puntoActual = puntoA;
            }
            else
            {
                puntoActual = puntoB;
            }

            // Voltear el sprite al cambiar de dirección
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    void Disparar()
    {
        if (Time.time >= tiempoSiguienteDisparo)
        {
            GameObject rocket = Instantiate(dropPrefab, transform.position, Quaternion.identity);
            tiempoSiguienteDisparo = Time.time + tiempoEntreDisparos;
            Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, -velocidadDisparo);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(puntoA.position, 0.5f);
        Gizmos.DrawWireSphere(puntoB.position, 0.5f);
        Gizmos.DrawLine(puntoA.position, puntoB.position);
    }
}