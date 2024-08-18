using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBee : MonoBehaviour
{
   // public float speed = 5f; // Velocidad de movimiento de la abeja
   // public float radiusX = 5f; // Radio del �valo en el eje X
   // public float radiusY = 2f; // Radio del �valo en el eje Y
    public GameObject stingerPrefab; // Prefab del aguij�n
    public float stingerSpeed = 10f; // Velocidad del aguij�n
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
        // Movimiento en un patr�n ovalado
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
        // Cambiar a la animaci�n de ataque
        animator.SetBool("isAttacking", true);

        // Crear el aguij�n en la posici�n de la abeja
        GameObject stinger = Instantiate(stingerPrefab, transform.position, Quaternion.identity);

        // Asignar la velocidad al aguij�n
        Rigidbody2D rb = stinger.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = -transform.up * stingerSpeed; // El aguij�n se lanza hacia abajo (ajustar seg�n la orientaci�n de tu juego)
        }

        // Esperar un peque�o tiempo antes de volver a Idle
        yield return new WaitForSeconds(0.5f); // Ajusta el tiempo seg�n la duraci�n de tu animaci�n de ataque

        // Volver a la animaci�n Idle
        animator.SetBool("isAttacking", false);

        // Destruir el aguij�n despu�s de un tiempo para evitar sobrecarga
        Destroy(stinger, 5f);
    }
}
