using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    private Transform player;               // Reference to the player's position.
    private NavMeshAgent nav;               // Reference to the nav mesh agent.
    private IEnumerator coroutine;
    GameObject accelera;
    [SerializeField]
    private float maxSpeed = 5f;
    private float movementSpeed = 3f;
    private bool movementEnabled = true;
    
    private void Start()
    {
        player = Player.Instance.transform;
        nav = GetComponent<NavMeshAgent>();
        
        nav.speed = movementSpeed;
    }

    private void Update()
    {
        if (!movementEnabled)
            return;

        if (movementSpeed < maxSpeed)
        {
            movementSpeed = Mathf.Lerp(movementSpeed, maxSpeed, Time.deltaTime);
            nav.speed = movementSpeed;
        }
        
        nav.SetDestination(player.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.HasFather())
            {
                GameManager.Instance.RemoveFather();
            }
            other.enabled = false;
            ObjectPool.Get<GameOver>();
//            SceneManager.LoadScene("house");
        }
    }
    public void DecreaseSpeed()
    {
        movementSpeed /= 2;
    }

    public void Stop()
    {
        nav.Stop();
        movementSpeed = 0;
    }

    public void EnableMovement(bool action)
    {
        movementEnabled = action;

        if (action)
            nav.Resume();
        else
            nav.Stop();
    }
}
