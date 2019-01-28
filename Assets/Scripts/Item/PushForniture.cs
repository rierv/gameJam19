using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushForniture : MonoBehaviour, IInteractable
{
    public float pullStrenght;
    public float distance = 1;

    Rigidbody _rgbd;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        _rgbd = GetComponent<Rigidbody>();
        player = GameObject.FindObjectOfType<Player>();
    }

    public void Interact()
    {
        _rgbd.AddForce((player.transform.forward + player.transform.up*pullStrenght/1100) * pullStrenght);
    }

    public InteractionMode InteractionMode {
        get { return InteractionMode.INTERACT; }
    }
}
