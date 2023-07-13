using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5f; // The movement speed of the player
    private float jumpForce = 10f; // The force applied when the player jumps
    public float turnSpeed = 2f; // The rotation speed of the player

    Quaternion right = Quaternion.Euler(0f, 90f, 0f); // The rotation when turning right
    Quaternion left = Quaternion.Euler(0f, -90f, 0f); // The rotation when turning left
    Quaternion behind = Quaternion.Euler(0f, 180f, 0f); // The rotation when turning back
    string OBSTACLE_TAG = "Obstacle"; // The tag of the obstacle objects

    public static bool isFalling = false; // Indicates whether the player is falling or not

    private Rigidbody myBody;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to the player
    }

    void Update()
    {
        if (!GameController.instance.isPaused) // Check if the game is not paused
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                transform.position += transform.forward * moveSpeed * Time.deltaTime; // Move the player in the forward direction based on input and movement speed
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                Quaternion targetRotation = transform.rotation * left; // Calculate the target rotation when turning left
                StartCoroutine(SmoothRotate.instance.SmoothRotation(transform, targetRotation, turnSpeed)); // Start a coroutine to smoothly rotate the player towards the target rotation
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Quaternion targetRotation = transform.rotation * right; // Calculate the target rotation when turning right
                StartCoroutine(SmoothRotate.instance.SmoothRotation(transform, targetRotation, turnSpeed)); // Start a coroutine to smoothly rotate the player towards the target rotation
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Quaternion targetRotation = transform.rotation * behind; // Calculate the target rotation when turning back
                StartCoroutine(SmoothRotate.instance.SmoothRotation(transform, targetRotation, turnSpeed)); // Start a coroutine to smoothly rotate the player towards the target rotation
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!isFalling) // Check if the player is not already falling
                {
                    isFalling = true; // Set the isFalling flag to true
                    myBody.AddForce(transform.up * jumpForce, ForceMode.Impulse); // Apply an upward force to make the player jump
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(OBSTACLE_TAG)) // Check if the player collided with an obstacle
        {
            isFalling = false; // Set the isFalling flag to false since the player is no longer falling
        }
    }
}
