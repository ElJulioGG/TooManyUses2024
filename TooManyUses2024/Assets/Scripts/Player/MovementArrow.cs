using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private float timer;
    public Animator handAnimator;



    ///Booleans
    private bool isHolding;
    public bool onGround = true;

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
        handAnimator.SetFloat("Velocity", playerRb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;  // Record the time when the spacebar is pressed
        }
        else
        {
            jumpBufferCounter -=Time.deltaTime;
        }
        if (onGround&& (playerRb.velocity.y == 0))
        {
            attack1.ammo = 0;
            handAnimator.SetBool("onGround", true);
            playerRb.gravityScale = originalGravityScaleChange;
            if ((jumpBufferCounter>0 ) && (timer >= jumpDelay))
            {
                jumpBufferCounter = 0;
                isHolding = true;

            }
            if (Time.time - lastJumpPressTime <= jumpBufferTime && !isHolding)
            {
                lastJumpPressTime = -jumpBufferTime;  // Reset buffer time
            }
            if (Input.GetKeyUp(KeyCode.Space)&& isHolding)
            {
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
            
        }
      
           
    }
    private void addForce()
    {
        if (baseForceMagnitude <= maxChangeForceMagnitude)
        {
            baseForceMagnitude += Time.deltaTime * changeForceMagnitude;
        }
    }
    private void giveAmmo()
    {
        attack1.ammo = 1;
    }

}



