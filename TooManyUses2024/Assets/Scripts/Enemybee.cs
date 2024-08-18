using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBee : MonoBehaviour
{
   // public float speed = 5f; // Velocidad de movimiento de la abeja
   // public float radiusX = 5f; // Radio del óvalo en el eje X
   // public float radiusY = 2f; // Radio del óvalo en el eje Y
    public GameObject stingerPrefab; // Prefab del aguijón
    public float stingerSpeed = 10f; // Velocidad del aguijón
    public float fireRate = 2f; // Frecuencia con la que la abeja lanza aguijones

    private float angle = 0f;
    private float nextFire = 0f;
    private Animator animator; // Referencia al componente Animator

    void Start()
    {
        animator = GetComponent<Animator>(); // Obtener el Animator adjunto al objeto
    }

    void Update()
    {
        // Movimiento en un patrón ovalado
       // angle += speed * Time.deltaTime;
       // float x = Mathf.Cos(angle) * radiusX;
       // float y = Mathf.Sin(angle) * radiusY;
       // transform.position = new Vector3(x, y, transform.position.z);

        // Disparar aguijones a intervalos regulares
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(ShootStinger());
        }
    }

    IEnumerator ShootStinger()
    {
        // Cambiar a la animación de ataque
        animator.SetBool("isAttacking", true);

        // Crear el aguijón en la posición de la abeja
        GameObject stinger = Instantiate(stingerPrefab, transform.position, Quaternion.identity);

        // Asignar la velocidad al aguijón
        Rigidbody2D rb = stinger.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = -transform.up * stingerSpeed; // El aguijón se lanza hacia abajo (ajustar según la orientación de tu juego)
        }

        // Esperar un pequeño tiempo antes de volver a Idle
        yield return new WaitForSeconds(0.5f); // Ajusta el tiempo según la duración de tu animación de ataque

        // Volver a la animación Idle
        animator.SetBool("isAttacking", false);

        // Destruir el aguijón después de un tiempo para evitar sobrecarga
        Destroy(stinger, 5f);
    }
}
