using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyRocketShooter : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public RectTransform exclamationIndicator; // Objeto de exclamaci�n en la UI
    public GameObject rocketPrefab; // Prefab del cohete
    public Transform firePoint; // Punto desde el cual se disparar� el cohete
    public float rocketSpeed = 10f; // Velocidad del cohete
    public float followDelay = 2f; // Tiempo que el enemigo tarda en alcanzar la posici�n X del jugador
    public float exclamationTime = 1f; // Tiempo que se muestra la exclamaci�n antes de disparar
    public float shootInterval = 7f; // Intervalo de tiempo entre disparos

    private Vector3 targetPosition;
    private float nextShootTime = 0f;

    private void Start()
    {
        
    }
    void Update()
    {
        // Calcula la posici�n objetivo con retardo
        targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);

        // Convierte la posici�n del objetivo en coordenadas de la pantalla
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetPosition);

        // Mueve el indicador de exclamaci�n y el punto de disparo hacia la posici�n X del jugador con un retardo
        MoveToTarget(exclamationIndicator, screenPosition);
        MoveToTarget(firePoint, targetPosition);

        // Mueve el enemigo hacia la posici�n X del jugador con un retardo
        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(targetPosition.x, transform.position.y, transform.position.z),
            Time.deltaTime / followDelay * Mathf.Abs(transform.position.x - targetPosition.x)
        );

        // Dispara el cohete cuando el indicador de exclamaci�n y el punto de disparo est�n cerca de la posici�n X del jugador y ha pasado el tiempo de recarga
        if (Mathf.Abs(exclamationIndicator.position.x - screenPosition.x) < 0.1f &&
            Mathf.Abs(firePoint.position.x - targetPosition.x) < 0.1f &&
            Time.time >= nextShootTime)
        {
            StartCoroutine(ShootRocketAfterDelay());
            nextShootTime = Time.time + shootInterval; // Establece el pr�ximo tiempo de disparo
        }
    }

    IEnumerator ShootRocketAfterDelay()
    {
        exclamationIndicator.gameObject.SetActive(true); // Muestra el indicador de exclamaci�n
        yield return new WaitForSeconds(exclamationTime); // Espera el tiempo de exclamaci�n

        ShootRocket();
        exclamationIndicator.gameObject.SetActive(false); // Oculta el indicador de exclamaci�n
    }

    void ShootRocket()
    {
        // Instancia el cohete en el punto de disparo
        GameObject rocket = Instantiate(rocketPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -rocketSpeed); // Dispara el cohete hacia abajo
    }

    void MoveToTarget(Transform target, Vector3 screenPosition)
    {
        Vector3 targetScreenPosition = Camera.main.WorldToScreenPoint(target.position);
        target.position = Vector3.MoveTowards(
            target.position,
            new Vector3(screenPosition.x, target.position.y, target.position.z),
            Time.deltaTime / followDelay * Mathf.Abs(targetScreenPosition.x - screenPosition.x)
        );
    }
}
