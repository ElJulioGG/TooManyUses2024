using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private int damage = 10; // Daño que causa la bala
    [SerializeField] private float lifeTime = 3f; // Tiempo de vida de la bala

    void Start()
    {
        // Destruye la bala después de 'lifeTime' segundos si no colisiona con nada
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Si la bala colisiona con un enemigo
        if (collision.CompareTag("Enemy"))
        {
            // Aplica daño al enemigo
            collision.GetComponent<EnemyHealth>().TakeDamage(damage);
            // Destruye la bala
            Destroy(gameObject);
        }
    }
}
