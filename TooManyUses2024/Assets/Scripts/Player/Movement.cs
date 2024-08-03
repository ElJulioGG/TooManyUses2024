using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [Header("Movement Settings")]
    [SerializeField] private float velocidad;
    [SerializeField] private float velocityMoventBase;
    [SerializeField] private float velocityExtra;


    [Header("Jump Settings")]
    [SerializeField] private float forceJump;
    [SerializeField] private LayerMask MaskFlood;
    private int jumpsRestants;


    [Header("Sprint Settings")]
    public int runMax;
    private bool run = true;
    private int runsRemaining;


    private bool WatchRight;
    private new Rigidbody2D rigidbody;

    private void Start()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        runsRemaining = runMax;

    }

    private void Update()
    {
        HandleMovement();
        HandleRun();
        HandleJump();
    }

    private void HandleMovement()
    {
        float inputMovimiento = Input.GetAxis("Horizontal");
        Vector2 targetVelocity = new Vector2(inputMovimiento * velocidad, rigidbody.velocity.y);
        Vector2 currentVelocity = rigidbody.velocity;

        rigidbody.AddForce((targetVelocity - currentVelocity));
        GestionarOrientacion(inputMovimiento);
    }

    private void HandleRun()
    {
        if (IsOnGround())
        {
            runsRemaining = runMax;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && run && runsRemaining > 0)
        {
            runsRemaining--;
            velocidad = velocityExtra;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            velocidad = velocityMoventBase;
            run = true;
        }
    }

    private void HandleJump()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {

            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f);
            rigidbody.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
        }
    }

    private bool IsOnGround()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.down, 1f, MaskFlood);
        return raycastHit.collider != null;
    }

    void GestionarOrientacion(float inputMovimiento)
    {
        if ((WatchRight == true && inputMovimiento > 0) || (WatchRight == false && inputMovimiento < 0))
        {
            WatchRight = !WatchRight;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
}
