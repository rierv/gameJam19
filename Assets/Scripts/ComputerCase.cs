using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerCase : MonoBehaviour, IInteractable
{
    [SerializeField] private Computer comp;
    public void Interact()
    {
        if (comp != null)
        {
            comp.Interact();
        }
    }

    public InteractionMode InteractionMode {
        get { return InteractionMode.INTERACT; }
    }
}
