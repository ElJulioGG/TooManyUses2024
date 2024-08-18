using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionSceneChanger : MonoBehaviour
{
    public List<GameObject> objectsToDeactivate; // Lista de objetos a desactivar
    private BoxCollider2D boxCollider; // Variable privada para almacenar el BoxCollider2D

    void Start()
    {
        // Obtener el BoxCollider2D adjunto al objeto
        boxCollider = GetComponent<BoxCollider2D>();

        // Inicializa la lista si no est� asignada en el Inspector
        if (objectsToDeactivate == null)
        {
            objectsToDeactivate = new List<GameObject>();
        }

        // Asegurarse de que el BoxCollider2D est� configurado como Trigger
        if (boxCollider != null)
        {
            boxCollider.isTrigger = true;
        }
        else
        {
            Debug.LogWarning("No se encontr� un BoxCollider2D en este objeto.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Desactiva todos los objetos en la lista cuando el trigger es activado
        foreach (GameObject obj in objectsToDeactivate)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }

        // Cambia a la escena "Stewei"
        SceneManager.LoadScene("Stewei");
    }
}
