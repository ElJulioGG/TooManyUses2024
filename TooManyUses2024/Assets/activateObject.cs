using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateObject : MonoBehaviour
{
    public GameObject obj;
    public GameObject confetti;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            obj.SetActive(true);
            confetti.SetActive(true);
            GameManager.instance.playerCanMove = false;
            GameManager.instance.playerCanAtack = false;
            AudioManager.instance.musicSource.Stop();
            AudioManager.instance.PlayMusic("VictoryMusic");
        }
    }
}
