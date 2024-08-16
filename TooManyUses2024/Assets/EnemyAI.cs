using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    private Transform target; // This will dynamically change between player and the game object with the radius
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float updatePathRate = 0.5f;

    public Transform enemySprite;

    public GameObject radiusObject; // The game object that has the radius (CircleCollider2D)
    private CircleCollider2D detectionRadius; // The CircleCollider2D component attached to the radiusObject

    private Transform defaultTarget; // The game object with the radius
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        // Get the CircleCollider2D from the radiusObject
        detectionRadius = radiusObject.GetComponent<CircleCollider2D>();

        // Set the default target to the object with the radius
        defaultTarget = radiusObject.transform;

        // Initially set the target to the default target
        target = defaultTarget;

        InvokeRepeating("UpdatePath", 0f, updatePathRate);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        CheckForPlayer();

        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (rb.velocity.x >= 0.01f)
        {
            enemySprite.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            enemySprite.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    void CheckForPlayer()
    {
        // Find the player
        Transform playerTransform = GameObject.FindWithTag("Player").transform;

        // Check if the player is within the bounds of the CircleCollider2D
        if (detectionRadius.bounds.Contains(playerTransform.position))
        {
            target = playerTransform; // If player is within the collider, chase the player
        }
        else
        {
            target = defaultTarget; // If player is outside the collider, follow the default target
        }
    }
}
