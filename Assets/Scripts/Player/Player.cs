using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance
    {
        get;
        private set;
    }

    [SerializeField] private Transform itemPivot = null;
    [SerializeField] private float throwForce = 50f;
    [SerializeField][Range(1.7f, 10f)] private float raycastHitDistance = 1.7f;
    
    [SerializeField] private KeyCode iteractionKey = KeyCode.V;
    [SerializeField] private KeyCode throwKey = KeyCode.B;
    [SerializeField] private KeyCode raycastItemKey = KeyCode.Mouse0;
    
    private PlayerItemTrigger itemTrigger = null;
    private PlayerMovement movement = null;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        itemTrigger = GetComponentInChildren<PlayerItemTrigger>();
        movement = GetComponentInChildren<PlayerMovement>();
        
        itemTrigger.SetPlayer(this);
        
        itemTrigger.SetThrowForce(throwForce);
        itemTrigger.SetRaycastHitDistance(raycastHitDistance);
        
        itemTrigger.SetGrabKey(iteractionKey);
        itemTrigger.SetRaycastKey(raycastItemKey);
        itemTrigger.SetThrowKey(throwKey);
    }

    public void OnItemGrab(Holdable holdable)
    {
        holdable.transform.SetParent(itemPivot);
        holdable.transform.localPosition = Vector3.zero;
        holdable.OnGrab();
        movement.speed /= 1.5f;
    }

    public void OnItemRelease()
    {
        movement.speed *= 1.5f;
    }

    public PlayerMovement GetMovement()
    {
        return movement;
    }

    public CharacterController GetCharacterController()
    {
        return GetComponent<CharacterController>();
    }

    public PlayerItemTrigger GetItemTrigger()
    {
        return itemTrigger;
    }
}
