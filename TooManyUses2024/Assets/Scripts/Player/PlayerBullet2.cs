using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet2 : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private GameObject Player;
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    [SerializeField] private float BulletForce;

    public int damage = 100;
    public int magnitude = 15;
   // public GameObject breakParticles;
    [SerializeField] private float destroyTime = 1f;


    // Start is called before the first frame update
    void Start()
    {
        //AudioManager.instance.PlaySfx("PearlThrow");
        Player = GameObject.FindGameObjectWithTag("Player");
        playerRb = Player.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();

        Vector3 direction = Vector3.zero;
        Vector3 rotation = Vector3.zero;

        // Determine direction based on arrow key input
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction = Vector3.left;
            rotation = Vector3.right;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            direction = Vector3.right;
            rotation = Vector3.left;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            direction = Vector3.up;
            rotation = Vector3.down;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            direction = Vector3.down;
            rotation = Vector3.up;
        }

        // Apply the bullet's velocity and rotation if any arrow key was pressed
        if (direction != Vector3.zero)
        {
            rb.velocity = direction.normalized * BulletForce;
            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot + 90);

            // Apply recoil to the player in the opposite direction of the bullet
            Vector2 recoilDirection = -direction.normalized;
            playerRb.AddForce(recoilDirection * magnitude, ForceMode2D.Impulse);
        }

        // Invoke destruction event after set time
        Invoke("destroyEvent", destroyTime);

        // AudioManager.instance.PlaySfxLoop1("AirTimeProyectile");


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Player.transform.position = gameObject.transform.position;
        //if (GameManager.instance.playerDied)
        //{
        //    destroyEvent();
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            destroyEvent();
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Melee")
        {
            destroyEvent();
        }
    }

    private void destroyEvent()
    {
        // AudioManager.instance.PlaySfx("Negative");
       // Instantiate(breakParticles, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
        //AudioManager.instance.sfxLoopSource1.Stop();
        //AudioManager.instance.PlaySfx4("ImpactProyectile");
    }
    //s
}
