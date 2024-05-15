using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public AudioClip hurtSound; // Audio clip for hurt sound
    private int health;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Hurt(int damage)
    {
        if (health > 0) {
            health -= damage;
            Debug.Log($"Health: {health}");
            // Play hurt sound if the hurt audio clip is assigned
            if (hurtSound != null)
            {
                audioSource.PlayOneShot(hurtSound);
            }
        } else {
            Debug.Log($"I died");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ammo"))
        {
            Destroy(other.gameObject);
        } else {
            print("uh oh");
        }
    }



}
