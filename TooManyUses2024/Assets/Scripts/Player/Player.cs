using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private Movement2 movement2;
    [SerializeField]private Attack1 attack1;
    [SerializeField] private Attack2 attack2;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        GameManager.instance.playerCanAtack = true;
        GameManager.instance.playerCanMove = true;
        GameManager.instance.playerHasBeenHit = false;
        GameManager.instance.playerIsInvincible= false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")&& rb.velocity.y ==0)
        {
            movement2.onGround = true;
            attack1.waitForAtack1 =false;
            attack2.waitForAtack1 = false;
            AudioManager.instance.PlaySfx("Fall");
            AudioManager.instance.FootStepsSource.Stop();
        }
       
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            movement2.onGround = false;
        }
    }

}
