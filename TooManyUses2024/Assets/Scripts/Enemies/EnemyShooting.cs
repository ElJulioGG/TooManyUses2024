using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float shootingInterval = 2f;

    private Transform player;
    private float nextShootTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nextShootTime = Time.time;
    }

    void Update()
    {
        if (Time.time > nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootingInterval;
        }
    }

    private void Shoot()
    {
        if (player == null) return;

        Vector2 direction = (player.position - firePoint.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}
