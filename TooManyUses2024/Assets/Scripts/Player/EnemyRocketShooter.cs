using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyRocketShooter : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public Transform exclamationIndicator; // Objeto de exclamación
    public GameObject rocketPrefab; // Prefab del cohete
    public Transform firePoint; // Punto desde el cual se disparará el cohete
    public float rocketSpeed = 10f; // Velocidad del cohete
    public float followDelay = 2f; // Tiempo que el enemigo tarda en alcanzar la posición X del jugador
    public float exclamationTime = 1f; // Tiempo que se muestra la exclamación antes de disparar
    public float shootInterval = 7f; // Intervalo de tiempo entre disparos

    private Vector3 targetPosition;
    private float nextShootTime = 0f;

    void Update()
    {
        // Calcula la posición objetivo con retardo
        targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
        exclamationIndicator.position = new Vector3(targetPosition.x, exclamationIndicator.position.y, exclamationIndicator.position.z);

        // Mueve el enemigo hacia la posición X del jugador con un retardo
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, transform.position.y, transform.position.z), Time.deltaTime / followDelay * Mathf.Abs(transform.position.x - targetPosition.x));

        // Dispara el cohete cuando el enemigo está cerca de la posición X del jugador y ha pasado el tiempo de recarga
        if (Mathf.Abs(transform.position.x - targetPosition.x) < 0.1f && Time.time >= nextShootTime)
        {
            StartCoroutine(ShootRocketAfterDelay());
            nextShootTime = Time.time + shootInterval; // Establece el próximo tiempo de disparo
        }
    }

    IEnumerator ShootRocketAfterDelay()
    {
        exclamationIndicator.gameObject.SetActive(true); // Muestra el indicador de exclamación
        yield return new WaitForSeconds(exclamationTime); // Espera el tiempo de exclamación

        ShootRocket();
        exclamationIndicator.gameObject.SetActive(false); // Oculta el indicador de exclamación
    }

    void ShootRocket()
    {
        // Instancia el cohete en el punto de disparo
        GameObject rocket = Instantiate(rocketPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -rocketSpeed); // Dispara el cohete hacia abajo
    }
}