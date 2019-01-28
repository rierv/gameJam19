using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillowTrip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate((Vector3.right + Vector3.up)*Random.Range(-100f,100f));
    }
}
