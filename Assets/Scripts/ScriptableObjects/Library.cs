using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Library : MonoBehaviour, IInteractable
{
    Vector3 move;

    public void Interact()
    {
        move = new Vector3(50f, 50f, 50f);
        move = transform.TransformDirection(move);
    }

    public InteractionMode InteractionMode => (InteractionMode.INTERACT);
}
