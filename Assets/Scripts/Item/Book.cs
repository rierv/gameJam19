using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    bool ready = false;
    public Text text;
    public float timer;
    public bool textEnabled;
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        if (textEnabled)
        {
            timer += Time.deltaTime;
            if (timer >= 5)
            {
                timer = 0;
                text.text = "";
                textEnabled = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1);
                int i = 0;
                while (i < hitColliders.Length)
                {
                    if (hitColliders[i].CompareTag("television"))
                    {
                        return;
                    }
                    i++;
                }   
                text.text = "Two years ago my parents took me to a shrink. She told me to keep a diary. It didn't last too long...";
                textEnabled = true;
            }
        }
    }
}
