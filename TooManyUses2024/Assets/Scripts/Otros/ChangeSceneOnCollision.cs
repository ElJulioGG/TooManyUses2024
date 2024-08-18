using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneAndDeactivate : MonoBehaviour
{
    // Referencia al GameObject que deseas desactivar
    public GameObject objectToDeactivate;

    private void Start()
    {
        objectToDeactivate.SetActive(true);
    }
    private void Update()
    {
        objectToDeactivate.SetActive(true);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        objectToDeactivate.SetActive(false);
        // Verifica si la colisión es con el jugador que tiene el tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Desactiva el GameObject especificado
            if (objectToDeactivate != null)
            {
                objectToDeactivate.SetActive(false);
            }

            // Cambia a la escena "Stewei"
            SceneManager.LoadScene("Stewei");
        }
    }
}
