using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFliying3 : MonoBehaviour
{

    public GameObject dropPrefab; // Prefab del huevo a disparar
    public float tiempoEntreDisparos; // Tiempo entre disparos
    private float tiempoSiguienteDisparo;

    void Start()
    {
        tiempoSiguienteDisparo = Time.time + tiempoEntreDisparos;
    }

    void Update()
    {
        Disparar();
    }


    void Disparar()
    {
        if (Time.time >= tiempoSiguienteDisparo)
        {
            // Instanciar un huevo y dispararlo hacia abajo
            Instantiate(dropPrefab, transform.position, Quaternion.identity);
            tiempoSiguienteDisparo = Time.time + tiempoEntreDisparos;
        }
    }
}