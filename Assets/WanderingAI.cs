using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{


    public float speed = 1.0f;
    public float detectionRange = 3.0f; // Range within which the enemy detects the player

    public AudioClip[] audioClips;

    private bool isAlive;

    private Transform player; 
    private UnityEngine.AI.NavMeshAgent agent;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = player.position;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (isAlive) {
            if (distanceToPlayer <= detectionRange) {
                if (!audioSource.isPlaying && audioClips.Length > 0)
                {
                    int randomIndex = Random.Range(0, audioClips.Length);
                    audioSource.clip = audioClips[randomIndex];
                    audioSource.Play();
                }
            }

            agent.destination = player.position;

        }

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

