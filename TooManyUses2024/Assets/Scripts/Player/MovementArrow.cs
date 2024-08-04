using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{
    [Header("ArrowAtributes")]
    [SerializeField] float currentAngle;
    public float angle1 = -45f;
    public float angle2 = 45f;
    public float speed = 2f;
    private float t = 0f;

    [Header("PlayerAtributes")]
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private float initialForceMagnitude;
    [SerializeField] private float baseForceMagnitude;
    [SerializeField] private float changeForceMagnitude;
    [SerializeField] private float maxChangeForceMagnitude;
    [SerializeField] private float originalGravityScaleChange = 1.1f;
    [SerializeField] private float gravityScaleChange = 1.1f;

    public Animator handAnimator;



    ///Booleans
    private bool isHolding;
    public bool onGround = true;

    void Start()
    {
        gravityScaleChange = originalGravityScaleChange;
        baseForceMagnitude = initialForceMagnitude;
        isHolding = false;
    }

    void Update()
    {
        handAnimator.SetFloat("Velocity", playerRb.velocity.y);
        if (onGround)
        {
            handAnimator.SetBool("onGround", true);
            playerRb.gravityScale = originalGravityScaleChange;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isHolding = true;
                handAnimator.SetBool("isHolding", true);

            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                isHolding = false;
                handAnimator.SetBool("isHolding", false);
                applyForce();
            }
            if (isHolding)
            {
                addForce();
            }
            else
            {
                changeDirection();
            }
        }
        else
        {
             handAnimator.SetBool("onGround", false); 
            
            playerRb.gravityScale = playerRb.gravityScale * gravityScaleChange;
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
       }
      
           
    }
    private void addForce()
    {
        if (baseForceMagnitude <= maxChangeForceMagnitude)
        {
            baseForceMagnitude += Time.deltaTime * changeForceMagnitude;
        }
    }

}



