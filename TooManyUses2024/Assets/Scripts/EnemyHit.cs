using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public float forceAmount = 10f; // Amount of force to apply

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !GameManager.instance.playerIsInvincible)
        {
            GameManager.instance.playerHasBeenHit = true;
            print("has been hit");

            // Get the Rigidbody2D of the colliding object
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Generate a random direction
                Vector2 randomDirection = Random.insideUnitCircle.normalized;

                // Apply the force to the Rigidbody2D in the random direction
                rb.AddForce(randomDirection * forceAmount, ForceMode2D.Impulse);
            }
            AudioManager.instance.PlaySfx("GetHit");
        }
    }
}
