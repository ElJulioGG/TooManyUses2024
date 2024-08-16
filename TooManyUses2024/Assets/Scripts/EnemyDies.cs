using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDies : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            enemyKilled();
        }
    }


    private void enemyKilled()
    {
        Destroy(gameObject);
        AudioManager.instance.PlaySfx("Squeak");
    }
}
