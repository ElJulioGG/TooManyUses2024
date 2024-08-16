using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2 : MonoBehaviour
{
    public GameObject Player;
    public Rigidbody2D playerRb2D;
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    public float recoil;
    public float timer;
    public float timerBetweenFiring;
    [SerializeField] public int ammo = 0;
    [SerializeField] public float returnRotationDelay;
    private Vector3 newPosition;
    public bool waitForAtack1 = false;

    [SerializeField] private float offsetX = 0;
    [SerializeField] private float offsetY = 0;
    [SerializeField] private float offsetZ = 0;
    [SerializeField] private GameObject spawnParticles;

    public Animator playerAnimator;
    public GameObject spriteHand;

    [SerializeField] private float cooldown = 3.0f; // Cooldown time in seconds
    private float cooldownTimer = 0f;
    public bool cooldownReady = false;

    void Start()
    {
        waitForAtack1 = false;
        mainCam = Camera.main;
        cooldownReady = true; // Initial state allowing the first shot
    }

    void Update()
    {
        if (GameManager.instance.playerCanMove)
        {
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

            Vector2 direction = mousePos - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            if (!canFire && GameManager.instance.playerCanAtack)
            {
                timer += Time.deltaTime;
                if (timer > timerBetweenFiring && cooldownReady)
                {
                    canFire = true;
                    timer = 0;
                }
            }

            if (canFire && GameManager.instance.playerCanAtack && waitForAtack1 && cooldownReady)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    FireAllDirections();
                    playerAnimator.SetTrigger("Shoot");

                    spriteHand.transform.Rotate(0, 0, 180);
                    Invoke("returnRotation", returnRotationDelay);
                    canFire = false;
                    playerRb2D.velocity = Vector2.zero;
                    playerRb2D.gravityScale = 0.1f;
                    waitForAtack1 = false;
                    AudioManager.instance.PlaySfx("Fire");
                   
                    cooldownReady = false; // Set cooldownReady to false after firing
                    cooldownTimer = 0f; // Reset the cooldown timer
                }
            }

            if (!cooldownReady)
            {
                cooldownTimer += Time.deltaTime;
                if (cooldownTimer >= cooldown)
                {
                    cooldownReady = true; // Allow shooting again after cooldown
                    Instantiate(spawnParticles, Player.transform.position, Quaternion.identity);
                    AudioManager.instance.PlaySfx("FireCharge");
                }
            }

            newPosition = new Vector3(Player.transform.position.x + offsetX, Player.transform.position.y + offsetY, Player.transform.position.z + offsetZ);
            gameObject.transform.position = newPosition;

            if (ammo <= 0)
            {
                // gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        // ammo = 999;

        //Instantiate(spawnParticles, Player.transform.position, Quaternion.identity);
        //AudioManager.instance.PlaySfx("TransformEnd");
    }

    private void OnDisable()
    {
        // Instantiate(spawnParticles, Player.transform.position, Quaternion.identity);
        //AudioManager.instance.PlaySfx("TransformEnd");
    }

    void returnRotation()
    {
        spriteHand.transform.rotation = Quaternion.identity;
    }

    void FireAllDirections()
    {
        Vector2[] directions = new Vector2[]
        {
            Vector2.left,
            Vector2.right,
            Vector2.up,
            Vector2.down,
            new Vector2(-1, 1).normalized,  // Top-left
            new Vector2(1, 1).normalized,   // Top-right
            new Vector2(-1, -1).normalized, // Bottom-left
            new Vector2(1, -1).normalized   // Bottom-right
        };

        playerAnimator.SetTrigger("Shoot2");

        foreach (Vector2 dir in directions)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            GameObject newBullet = Instantiate(bullet, spriteHand.transform.position, Quaternion.Euler(0, 0, angle));
            Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = dir * recoil;
        }

        AudioManager.instance.PlaySfx("Electric");
        Invoke("returnRotation", returnRotationDelay);
    }
}
