using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FornitureFalls : MonoBehaviour, IInteractable
{
    Rigidbody _rgbd;
    Player player;
    public float pullStrenght = 30;
    public float distance = 2;
    // Start is called before the first frame update
    void Start()
    {
        _rgbd = GetComponent<Rigidbody>();
        player = GameObject.FindObjectOfType<Player>();
    }

    public void Interact()
    {
        _rgbd.AddForce(-transform.up * pullStrenght);
    }

    public InteractionMode InteractionMode
    {
        get { return InteractionMode.INTERACT; }
    }
}
