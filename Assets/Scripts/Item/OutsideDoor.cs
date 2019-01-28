using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutsideDoor : MonoBehaviour
{
    public Text text;
    public float timer;
    public bool textEnabled;
    bool entered = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (textEnabled)
        {
            timer += Time.deltaTime;
            if (timer >= 2)
            {
                text.text = "";
                textEnabled = false;
                timer = 0;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                timer = 0;
                textEnabled = true;
                text.text = "Whatever happens, I will never leave this house!";
            }

        }
    }
}
