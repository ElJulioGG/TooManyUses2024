using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFliying : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad de movimiento del enemigo
    public GameObject dropPrefab; // Prefab del huevo a disparar
    public float tiempoEntreDisparos; // Tiempo entre disparos
    private float tiempoSiguienteDisparo;
    public float velocidadDisparo; // Velocidad del disparo


    void Start()
    {
        tiempoSiguienteDisparo = Time.time + tiempoEntreDisparos;
    }

    void Update()
    {
        Mover();
        Disparar();
    }

    void Mover()
    {
        // Movimiento simple hacia la izquierda
        transform.Translate(Vector2.left * velocidad * Time.deltaTime);
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
}