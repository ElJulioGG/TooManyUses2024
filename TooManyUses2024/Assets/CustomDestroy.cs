using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField ]private float destroyTime = 2f;
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
