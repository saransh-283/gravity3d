using System.Collections;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public GameObject hologramPrefab;
    GameController gameControllerInstance;

    Quaternion targetRotation;
    Quaternion right = Quaternion.Euler(0f, 0f, 90f);
    Quaternion left = Quaternion.Euler(0f, 0f, -90f);
    Quaternion top = Quaternion.Euler(0f, 0f, 180f);
    Vector3 newGravity;
    GameObject hologramInstance;

    private void Start()
    {
        // Get the GameController instance for reference
        gameControllerInstance = GameController.instance;
    }

    void Update()
    {
        if (!gameControllerInstance.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // Start coroutine to smoothly rotate the player and update gravity
                StartCoroutine(SmoothRotate.instance.SmoothRotation(transform, targetRotation, rotationSpeed));
                Physics.gravity = newGravity * GameController.instance.G;
                DestroyHologram();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                // Set target rotation and new gravity for right arrow key
                targetRotation = transform.rotation * right;
                newGravity = transform.right;
                SmoothRotateHologram(targetRotation);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                // Set target rotation and new gravity for left arrow key
                targetRotation = transform.rotation * left;
                newGravity = -transform.right;
                SmoothRotateHologram(targetRotation);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                // Set target rotation and new gravity for up arrow key
                targetRotation = transform.rotation * top;
                newGravity = transform.up;
                SmoothRotateHologram(targetRotation);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // Set target rotation and new gravity for down arrow key
                targetRotation = transform.rotation;
                newGravity = -transform.up;
                StartCoroutine(SmoothRotate.instance.SmoothRotation(hologramInstance.transform, targetRotation, rotationSpeed, DestroyHologram));
            }
        }
    }

    void DestroyHologram()
    {
        // Destroy the hologram instance
        Destroy(hologramInstance);
    }

    void SmoothRotateHologram(Quaternion targetRotation)
    {
        if (hologramInstance == null)
        {
            // Instantiate the hologram and set it as a child of the player
            hologramInstance = Instantiate(hologramPrefab, transform.position, transform.rotation);
            hologramInstance.transform.parent = transform;
        }

        // Start coroutine to smoothly rotate the hologram
        StartCoroutine(SmoothRotate.instance.SmoothRotation(hologramInstance.transform, targetRotation, rotationSpeed));
    }
}
