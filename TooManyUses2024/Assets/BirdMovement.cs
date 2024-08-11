using UnityEngine;

public class BirdMovementBetweenPoints : MonoBehaviour
{
    public GameObject pointA; // Punto de inicio.
    public GameObject pointB; // Punto de destino.
    public float speed = 5f;  // Velocidad del ave.

    private Vector3 targetPosition; // Posición objetivo.

    void Start()
    {
        // Establece la posición inicial del ave en pointA.
        transform.position = pointA.transform.position;

        // Elige el punto B como el objetivo inicial.
        targetPosition = pointB.transform.position;
    }

    void Update()
    {
        MoveBird();
    }

    void MoveBird()
    {
        // Mueve el ave hacia la posición objetivo.
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Si el ave llega al punto B, se destruye.
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
