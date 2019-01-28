using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Holdable : MonoBehaviour, IInteractable
{
    private Rigidbody _rgbd = null;
    private Collider _collider;
    
    private void Start()
    {
        _rgbd = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public void OnGrab()
    {
        _collider.enabled = false;
        _rgbd.isKinematic = true;
    }

    public void Drop()
    {
        _collider.enabled = true;
        _rgbd.isKinematic = false;
        transform.SetParent(null);
    }

    public void Throw(Vector3 direction, float force)
    {
        _collider.enabled = true;
        _rgbd.isKinematic = false;
        transform.SetParent(null);
        _rgbd.AddForce(direction * force, ForceMode.Impulse);
    }

    public void Interact()
    {
        
    }

    public InteractionMode InteractionMode
    {
        get { return InteractionMode.GRAB; }
    }
}
