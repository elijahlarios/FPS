using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class bossAI : MonoBehaviour
{


    public float speed = 1.0f;
    public float maxVolumeDistance = 10.0f; // Maximum distance at which the audio is audible at full volume
    public float detectionRange = 5.0f;
    public AudioClip[] audioClips;

    private bool isAlive;

    private Transform player; 
    private UnityEngine.AI.NavMeshAgent agent;
    private AudioSource audioSource;

    private Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = player.position;
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true; // Loop the audio
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (isAlive)
        {
            if (distanceToPlayer <= detectionRange) 
            {
                animator.SetTrigger("sword attack");
                
            } 
            else
            {
                animator.SetTrigger("Walking");
            }

            // Adjust audio volume based on distance
            float volume = Mathf.Clamp01(1.0f - distanceToPlayer / maxVolumeDistance);
            audioSource.volume = volume;

            // Play audio
            if (!audioSource.isPlaying && audioClips.Length > 0)
            {
                int randomIndex = Random.Range(0, audioClips.Length);
                audioSource.clip = audioClips[randomIndex];
                audioSource.Play();
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
                // Debug.Log($"enemy hit a wall ");

            }
        }
    }

    public void SetAlive(bool alive) {
        isAlive = alive;
    }
}

