using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{


    public float speed = 1.0f;
    public float obstacleRange = 5.0f;

    private bool isAlive;

    private Transform player; 
    private UnityEngine.AI.NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = player.position;

    }

    void Update()
    {
        if (isAlive)
        {
            // Vector3 direction = player.position - transform.position;
            // direction.y = 0; // ignore vert distance

            // // rotate to player
            // Quaternion targetRotation = Quaternion.LookRotation(direction);
            // transform.rotation = targetRotation;

            // // move zambie towards player
            // transform.Translate(0, 0, speed * Time.deltaTime);
        }
            agent.destination = player.position;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isAlive)
        {
            PlayerCharacter playerCharacter = collision.gameObject.GetComponent<PlayerCharacter>();
            if (playerCharacter != null)
            {
                playerCharacter.Hurt(1);


            } else {
                Debug.Log($"enemy hit a wall ");

            }
        }
    }


    public void SetAlive(bool alive) {
        isAlive = alive;
    }
}
