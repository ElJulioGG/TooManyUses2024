using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform platform;
    public Transform startPoint;
    public Transform endPoint;
    public float speed;

    Vector2 targetPos;
    int direction = 1;

    private void Start()
    {
        targetPos = endPoint.position;
    }
    private void OnDrawGizmos()
    {
        if(platform!=null && startPoint!=null &&endPoint != null)
        {
            Gizmos.DrawLine(platform.position, startPoint.position);
            Gizmos.DrawLine(platform.position, endPoint.position);
        }
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, startPoint.position) < .1f) targetPos = endPoint.position;
       
        if (Vector2.Distance(transform.position, endPoint.position) < .1f) targetPos = startPoint.position;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(this.transform);
            print("col");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
            print("col");
        }
    }
}
