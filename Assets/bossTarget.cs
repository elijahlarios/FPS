using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class bossTarget : MonoBehaviour
{
    public int maxHealth = 200;
    public int currentHealth;
    public HealthBar healthBar;
    public AudioClip hitSound; // Sound to play when hit
    public float hitSoundDelay = 2.0f; // Delay before playing hit sound again
    private float lastHitSoundTime; // Last time hit sound was played
    public enum HitDirection {
        Forward,
        Backward
    }
    private Animator animator;
    private AudioSource audioSource;

    void Start() {
        animator = GetComponent<Animator>();
        healthBar = FindObjectOfType<HealthBar>();
        if (healthBar != null) {
            healthBar.SetMaxHealth(maxHealth);
        } else {
            Debug.LogError("HealthBar not found in the scene.");
        }
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        audioSource = GetComponent<AudioSource>();
        lastHitSoundTime = -hitSoundDelay; // Ensure the hit sound can be played immediately
    }
    public void ReactToHit(HitDirection hitDirection, int Damage) {
        currentHealth -= Damage;
        healthBar.SetHealth(currentHealth);

        // Play hit sound with delay
        if (Time.time - lastHitSoundTime > hitSoundDelay && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
            lastHitSoundTime = Time.time;
        }
        // Disable the NavMeshAgent component
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent != null)
        {
            
        }

        bossAI behavior = GetComponent<bossAI>();
        if (behavior != null) {
        }   
        if(currentHealth < 0) 
        { 
            StartCoroutine(Die(hitDirection));
            agent.enabled = false;
            behavior.SetAlive(false);
        }
        
    }

    public IEnumerator Die(HitDirection hitDirection) {

        if (hitDirection == HitDirection.Forward)
        {
            animator.SetTrigger("Death");
        }
        else if (hitDirection == HitDirection.Backward)
        {
            animator.SetTrigger("Death");
        }

        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
        Destroy(healthBar.gameObject);
    }
}