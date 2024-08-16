using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement2 : MonoBehaviour
{
    [Header("ArrowAtributes")]
    [SerializeField] float currentAngle;
    
    public float angle1 = -45f;
    public float angle2 = 45f;
    public float maxSpeed = 2f;
    public float speed = 2f;
    public float holdSpeed = 0.2f;
    private float t = 0f;


    [SerializeField] private SpriteRenderer arrowSprite;
    [Header("PlayerAtributes")]
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private float initialForceMagnitude;
    [SerializeField] private float baseForceMagnitude;
    [SerializeField] private float changeForceMagnitude;
    [SerializeField] private float maxChangeForceMagnitude;
    [SerializeField] private float originalGravityScaleChange = 1.1f;
    [SerializeField] private float gravityScaleChange = 1.1f;
    [SerializeField] private float jumpDelay = 0.1f;
    [SerializeField] private float jumpBufferTime = 0.2f;
    [SerializeField] private float jumpBufferCounter;
    [SerializeField] private Attack1 attack1;
    [SerializeField] private Attack2 attack2;
    [SerializeField] public bool isDead;
    [SerializeField] public bool deadSequence = true;
    public float maxHeight { get; private set; } = 0f;




    [SerializeField] private float timer;
    public Animator handAnimator;

    [Header("External")]
    [SerializeField] private GameObject cameraBounds;
    [SerializeField] private float cameraBoundsOffsetY;
    [SerializeField] private GameObject deathHitbox;
    [SerializeField] private float deathHitboxOffsetY;
    [SerializeField] private GameObject deathCanvas;

    [Header("Materials")]
    [SerializeField] private PhysicsMaterial2D bouncyMaterial;
    [SerializeField] private PhysicsMaterial2D normalMaterial;

    ///Booleans
    private bool isHolding;
    public bool onGround = true;
    public bool landed = false;
    public bool playHold = false;

    private float lastJumpPressTime;

    void Start()
    {
        speed = maxSpeed;
        gravityScaleChange = originalGravityScaleChange;
        baseForceMagnitude = initialForceMagnitude;
        isHolding = false;
        lastJumpPressTime = -jumpBufferTime;
        
    }

    void Update()
    {
        if (isDead)
        {
            if (deadSequence)
            {
                deathCanvas.SetActive(true);
                deadSequence= false;
                CameraShake.Instance.shakeCamera(5f, .1f);
                // AudioManager.instance.musicSource.Stop();
                AudioManager.instance.PlaySfx("DeathJingle");
            }
        }else
        {
            float currentHeight = transform.position.y;

            // Update maxHeight if currentHeight is higher
            if (currentHeight > maxHeight)
            {
                maxHeight = currentHeight;
                cameraBounds.transform.position = new Vector3(cameraBounds.transform.position.x, currentHeight - cameraBoundsOffsetY, cameraBounds.transform.position.z);
                deathHitbox.transform.position = new Vector3(deathHitbox.transform.position.x, currentHeight - deathHitboxOffsetY, deathHitbox.transform.position.z);
            }


            handAnimator.SetFloat("Velocity", playerRb.velocity.y);
            if (GameManager.instance.playerCanMove)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    jumpBufferCounter = jumpBufferTime;  // Record the time when the spacebar is pressed
                }
                else
                {
                    //wda
                    jumpBufferCounter -= Time.deltaTime;
                }
                if (onGround)
                {
                    attack1.ammo = 0;
                    attack2.ammo = 0;
                    handAnimator.SetBool("onGround", true);
                    playerRb.gravityScale = originalGravityScaleChange;
                    if ((jumpBufferCounter > 0) && (timer >= jumpDelay))
                    {
                        jumpBufferCounter = 0;
                        isHolding = true;

                    }
                    if (Time.time - lastJumpPressTime <= jumpBufferTime && !isHolding)
                    {
                        lastJumpPressTime = -jumpBufferTime;  // Reset buffer time
                    }
                    if (Input.GetKeyUp(KeyCode.Space) && isHolding)
                    {
                        playHold = false;
                        AudioManager.instance.FootStepsSource.Stop();
                        isHolding = false;
                        handAnimator.SetBool("isHolding", false);
                        applyForce();
                    }
                    if (isHolding)
                    {
                        handAnimator.SetBool("isHolding", true);
                        addForce();
                        changeDirection();
                        speed = holdSpeed;
                        float normalizedForce = baseForceMagnitude / maxChangeForceMagnitude;
                        arrowSprite.transform.localScale = new Vector3(1 + normalizedForce, 1 + normalizedForce, arrowSprite.transform.localScale.z);
                        arrowSprite.color = Color.Lerp(Color.green, Color.red, normalizedForce);
                    }
                    else
                    {
                        changeDirection();
                    }
                    timer += Time.deltaTime;
                }
                else
                {
                    timer = 0f;
                    handAnimator.SetBool("onGround", false);

                    playerRb.gravityScale = playerRb.gravityScale * gravityScaleChange;
                    if (Input.GetKeyUp(KeyCode.Space))
                    {
                        lastJumpPressTime = -jumpBufferTime;
                    }
                }
            }
            if (GameManager.instance.playerHasBeenHit)
            {
                GameManager.instance.playerCanMove = false;
                isHolding = false;
                handAnimator.SetBool("isHolding", false);
               
                handAnimator.SetBool("onGround", false);

                speed = maxSpeed;
                gravityScaleChange = originalGravityScaleChange;
                baseForceMagnitude = initialForceMagnitude;
                lastJumpPressTime = -jumpBufferTime;
                StartCoroutine(hitCorrutine());
            }
        }

    }

    private IEnumerator hitCorrutine()
    {
        CameraShake.Instance.shakeCamera(5f, .1f);
        handAnimator.SetBool("Invincible", true);
        AudioManager.instance.PlaySfx("GetHit");
        GameManager.instance.playerHasBeenHit = false;
        GameManager.instance.playerCanMove = false;
        GameManager.instance.playerIsInvincible = true;
        isHolding = false;
        Collider2D collider = playerRb.GetComponent<Collider2D>();

        if (collider != null)
        {
            collider.sharedMaterial = bouncyMaterial;
        }
        playerRb.gravityScale = originalGravityScaleChange;
        yield return new WaitForSeconds(2f);

        handAnimator.SetBool("Invincible", false);
        GameManager.instance.playerIsInvincible = false;
        GameManager.instance.playerCanMove = true;
        if (collider != null)
        {
            collider.sharedMaterial = normalMaterial;
        }
    }
    private void changeDirection()
    {
        t += Time.deltaTime * speed;
         currentAngle = Mathf.Lerp(angle1, angle2, Mathf.PingPong(t, 1f));
        transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);

      
    }
    
    private void applyForce()
    {
       if(Input.GetKeyUp(KeyCode.Space)){
            float angleInRadians = (currentAngle) * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
            playerRb.AddForce(direction * baseForceMagnitude);
            baseForceMagnitude = initialForceMagnitude;
            speed = maxSpeed;
            attack1.waitForAtack1 = true;
            attack2.waitForAtack1 = true;
            AudioManager.instance.PlayFootSteps("AirTime");

            // Reset arrow sprite to normal scale and color
            arrowSprite.transform.localScale = new Vector3(1f, 1f, arrowSprite.transform.localScale.z);
            arrowSprite.color = Color.green;

        }
      
           
    }
    private void addForce()
    {
        if (baseForceMagnitude <= maxChangeForceMagnitude)
        {
            if (!playHold)
            {
                AudioManager.instance.PlayFootSteps("HoldJump");
                playHold = true;
            }
            
            baseForceMagnitude += Time.deltaTime * changeForceMagnitude;
        }
    }
    private void giveAmmo()
    {
        attack2.ammo = 1;
        attack1.ammo = 1;
    }

}



