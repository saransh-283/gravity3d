using System.Collections;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float rotationSpeed = 10f;
    GameController gameControllerInstance;

    private void Start()
    {
        gameControllerInstance = GameController.instance;
    }

    void Update()
    {
        if (!gameControllerInstance.isPaused)
        {
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                RotateRight();
                Physics.gravity = transform.right * gameControllerInstance.G;
                HologramRotate.DestroyHologram();
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                RotateLeft();
                Physics.gravity = -transform.right * gameControllerInstance.G;
                HologramRotate.DestroyHologram();
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                RotateUp();
                Physics.gravity = transform.up * gameControllerInstance.G;
                HologramRotate.DestroyHologram();
            }
        }
    }

    void RotateRight()
    {
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0f, 0f, 90f);
        StartCoroutine(SmoothRotate.instance.SmoothRotation(transform, targetRotation, rotationSpeed));
    }

    void RotateLeft()
    {
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0f, 0f, -90f);
        StartCoroutine(SmoothRotate.instance.SmoothRotation(transform, targetRotation, rotationSpeed));
    }

    void RotateUp()
    {
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0f, 0f, 180f);
        StartCoroutine(SmoothRotate.instance.SmoothRotation(transform, targetRotation, rotationSpeed));
    }
}
