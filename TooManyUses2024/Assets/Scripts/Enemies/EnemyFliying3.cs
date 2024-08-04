using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFliying3 : MonoBehaviour
{

    public GameObject dropPrefab; // Prefab del huevo a disparar
    public float tiempoEntreDisparos; // Tiempo entre disparos
    private float tiempoSiguienteDisparo;
    public float velocidadDisparo; // Velocidad del disparo
    public float velocidad; // Velocidad de movimiento del enemigo


    void Start()
    {
        tiempoSiguienteDisparo = Time.time + tiempoEntreDisparos;
    }

    void Update()
    {
        Disparar();
        Mover();
    }

    void Mover()
    {
        // Movimiento simple hacia la izquierda
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }

    void Disparar()
    {
        if (Time.time >= tiempoSiguienteDisparo)
        {
            // Instanciar un huevo y dispararlo hacia abajo
            GameObject rocket = Instantiate(dropPrefab, transform.position, Quaternion.identity);
            tiempoSiguienteDisparo = Time.time + tiempoEntreDisparos;
            // Instancia el cohete en el punto de disparo
            Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, -velocidadDisparo);


        }
    }
}