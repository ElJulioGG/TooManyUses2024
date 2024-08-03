using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    [SerializeField] private float damageDelay = 1f; 
    private bool canTakeDamage = true;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            currentHealth -= damage;
            canTakeDamage = false;
            StartCoroutine(DamageCooldown());

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(damageDelay);
        canTakeDamage = true;
    }

    private void Die()
    {
        Debug.Log("Player died!");
    }
}
