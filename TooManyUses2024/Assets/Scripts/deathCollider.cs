using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathCollider : MonoBehaviour
{
    public Movement2 movement2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            movement2.isDead = true;
            AudioManager.instance.PlaySfx("FallDeath");
        }
    }
}
