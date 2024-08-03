using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Player movement related variables
    [Header("Movement Settings")]
    [SerializeField] private float velocidad;
    [SerializeField] private float velocityMoventBase;
    [SerializeField] private float velocityExtra;

    // Sprint related variables
    [Header("Sprint Settings")]
    public int runMax;
    private bool run = true;
    private int runsRemaining;

    // Misc variables
    private bool WatchRight;
    private new Rigidbody2D rigidbody;

    private void Start()
    {
        InitializeComponents();
    }

    // Initialize all necessary components
    private void InitializeComponents()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        runsRemaining = runMax; // Initializes the number of times you can run
    }

    private void Update()
    {
        HandleMovement();
        HandleRun();
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
            runsRemaining--; // Reduces the number of times you can run
            velocidad = velocityExtra;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            velocidad = velocityMoventBase;
            run = true;
        }
    }

    void GestionarOrientacion(float inputMovimiento)
    {
        if ((WatchRight == true && inputMovimiento > 0) || (WatchRight == false && inputMovimiento < 0))
        {
            WatchRight = !WatchRight;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    private bool IsOnGround()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
        return raycastHit.collider != null;
    }
}
