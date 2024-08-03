using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private int damage = 10; // Da�o que causa la bala
    [SerializeField] private float lifeTime = 3f; // Tiempo de vida de la bala

    void Start()
    {
        // Destruye la bala despu�s de 'lifeTime' segundos si no colisiona con nada
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Si la bala colisiona con el jugador
        if (collision.CompareTag("Player"))
        {
            // Aplica da�o al jugador
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
            // Destruye la bala
            Destroy(gameObject);
        }
    }
}
