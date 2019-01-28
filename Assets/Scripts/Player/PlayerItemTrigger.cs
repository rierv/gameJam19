using System;
using UnityEngine;

public class PlayerItemTrigger : MonoBehaviour
{
    private float raycastHitDistance = 3;
    private const float GET_ITEM_DISTANCE = 2;

    private KeyCode iteractionKey = KeyCode.V;
    private KeyCode throwKey = KeyCode.B;
    private KeyCode raycastItemKey = KeyCode.Mouse0;

    private Player player = null;
    private Holdable _equippedHoldable = null;
    private float throwForce = 15f;

    private void Update()
    {
        //grabs and holds a near Item with the ThrowableItem Tag.
        if (!Equiped())
        {
            //NearItem();
            RaycastForItem();
        }

        //leaves the equipped item
        if (Input.GetKeyUp(iteractionKey) && Equiped())
        {
            _equippedHoldable.Drop();
            ReleaseItem();
            
        }

        //shoots the equipped item 
        if (Input.GetKeyUp(throwKey) && Equiped())
        {
            _equippedHoldable.Throw(transform.forward, throwForce);
            ReleaseItem();
        }
    }

    private void NearItem()
    {
        //Check for nearest item: <Item>
        Holdable holdable = WorldItems.Instance.GetNearest(transform.position, GET_ITEM_DISTANCE);

        if (holdable != null)
        {
            //Equip item
            Equip(holdable);
        }
    }

    private void RaycastForItem()
    {
        RaycastHit hit;
        Vector3 direction = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, direction, out hit, raycastHitDistance))
        {
            ActivateCursor(true);
            if (Input.GetKeyDown(iteractionKey))
            {
                OnRaycastObject(hit.transform.gameObject);
            }
            else if(Input.GetKeyDown(throwKey))
            {
                IInteractable interactable = hit.transform.gameObject.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    switch (interactable.InteractionMode)
                    {
                        case InteractionMode.GRAB:
                            Holdable holdable = interactable as Holdable;
                            holdable.Throw(transform.forward, throwForce);
                            break;
                    }
                }
            }
        }
        else
        {
            ActivateCursor(false);
        }
    }

    private void OnRaycastObject(GameObject obj)
    {
        IInteractable interactable = obj.GetComponent<IInteractable>();
        if (interactable != null)
        {
            switch (interactable.InteractionMode)
            {
                case InteractionMode.GRAB:
                    Holdable holdable = interactable as Holdable;
                    Equip(holdable);
                    break;
                case InteractionMode.INTERACT:
                    interactable.Interact();
                    break;
            }
        }
    }

    public bool Equiped()
    {
        return _equippedHoldable != null;
    }

    public void Equip(Holdable holdable)
    {
        _equippedHoldable = holdable;

        if (player != null)
        {
            player.OnItemGrab(_equippedHoldable);
        }
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public void SetThrowForce(float force)
    {
        throwForce = force;
    }
    
    public void SetThrowKey(KeyCode code)
    {
        throwKey = code;
    }
    
    public void SetGrabKey(KeyCode code)
    {
        iteractionKey = code;
    }
    
    public void SetRaycastKey(KeyCode code)
    {
        throwKey = code;
    }

    public void SetRaycastHitDistance(float distance)
    {
        raycastHitDistance = distance;
    }

    private void ReleaseItem()
    {
        _equippedHoldable = null;
        player.OnItemRelease();
    }

    private void ActivateCursor(bool action)
    {
        if (UserUI.Instance != null)
        {
            UserUI.Instance.ActivateCursor(action);
        }
    }
}
