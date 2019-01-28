using System.Collections;
using UnityEngine;

[PoolableObject("poolable/Father")]
public class Father : MonoBehaviour
{
    [SerializeField] private float throwForce = 30f;
    private EnemyMovement movement;
    private Rigidbody rgbd;
    
    private void Start()
    {
        movement = GetComponent<EnemyMovement>();
        rgbd = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        Vector3 vel = other.gameObject.GetComponent<Rigidbody>().velocity;
        if (!(vel!=null && vel.x<1&&vel.x>-1&&vel.z>-1 &&vel.z<1&&vel.y>-1&&vel.y<1))
        {
            rgbd.velocity = Vector3.zero;
            print(other.gameObject.GetComponent<Rigidbody>().velocity);
            rgbd.isKinematic = false;
            rgbd.velocity = Vector3.zero;
            rgbd.AddForce(-transform.forward * 2000);
            rgbd.freezeRotation = true;

            Holdable hold = other.gameObject.GetComponent<Holdable>();
            if (hold != null)
            {
                hold.Throw(RandomDirection(), throwForce);
            }
            StartCoroutine(Disable());
        }
    }

    private IEnumerator Disable() {
        //movement.Stop();
        //movement.EnableMovement(false);
        yield return new WaitForSeconds(0.5f);
        rgbd.isKinematic = true;
        rgbd.freezeRotation = false;
        //movement.EnableMovement(true);
    }

    private Vector3 RandomDirection()
    {
        Vector3 dir = transform.forward;
        dir.x = Random.Range(-1, 1);
        dir.y = Random.Range(-1, 1);
        return dir;
    }
}