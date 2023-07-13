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
        gameControllerInstance = GameController.instance;
    }

    void Update()
    {
        if (!gameControllerInstance.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine(SmoothRotate.instance.SmoothRotation(transform, targetRotation, rotationSpeed));
                Physics.gravity = newGravity * GameController.instance.G;
                DestroyHologram();
            }
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                targetRotation = transform.rotation * right;
                newGravity = transform.right;
                SmoothRotateHologram(targetRotation);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                targetRotation = transform.rotation * left;
                newGravity = -transform.right;
                SmoothRotateHologram(targetRotation);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                targetRotation = transform.rotation * top;
                newGravity = transform.up;
                SmoothRotateHologram(targetRotation);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                targetRotation = transform.rotation;
                newGravity = -transform.up;
                StartCoroutine(SmoothRotate.instance.SmoothRotation(hologramInstance.transform, targetRotation, rotationSpeed, DestroyHologram));
            }
        }
    }

    void DestroyHologram()
    {
        Destroy(hologramInstance);
    }

    void SmoothRotateHologram(Quaternion targetRotation)
    {
        if (hologramInstance == null)
        {
            hologramInstance = Instantiate(hologramPrefab, transform.position, transform.rotation);
            hologramInstance.transform.parent = transform;
        }

        StartCoroutine(SmoothRotate.instance.SmoothRotation(hologramInstance.transform, targetRotation, rotationSpeed));
    }
}
