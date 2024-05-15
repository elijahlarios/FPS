using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour
{
    public float speed = 3.0f;

    public float runSpeed = 5.0f;
    public float gravity = -9.8f;
    public AudioClip walkingSound;
    public float walkingSoundSpeed = 1.0f; // Speed of walking sound effect
    public float runningSoundSpeed = 2.0f; // Speed of running sound effect

    private CharacterController charController;
    private AudioSource audioSource;
    private bool isWalking = false;
    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is moving
        if (charController.velocity.magnitude > 0 && !isWalking)
        {
            isWalking = true;
            // Start playing walking sound effect
            PlayWalkingSound();
        }
        else if (charController.velocity.magnitude == 0 && isWalking)
        {
            isWalking = false;
            // Stop playing walking sound effect
            audioSource.Stop();
        }

        // Set speed based on player input
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : speed;

        float deltaX = Input.GetAxis("Horizontal") * currentSpeed;
        float deltaZ = Input.GetAxis("Vertical") * currentSpeed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        movement = Vector3.ClampMagnitude(movement, currentSpeed);
        movement.y = gravity;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        charController.Move(movement);
    }

    void PlayWalkingSound()
    {
        if (walkingSound != null)
        {
            audioSource.clip = walkingSound;
            audioSource.pitch = Input.GetKey(KeyCode.LeftShift) ? runningSoundSpeed : walkingSoundSpeed;
            audioSource.Play();
        }
    }
}
