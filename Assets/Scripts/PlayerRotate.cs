using System.Collections;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public GameObject hologramPrefab;
    GameController gameControllerInstance;

    Quaternion targetRotation;
    Vector3 newGravity;
    bool isHologramVisible = false;
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
                targetRotation = transform.rotation * Quaternion.Euler(0f, 0f, 90f);
                newGravity = transform.right;
                SmoothRotateHologram(targetRotation);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                targetRotation = transform.rotation * Quaternion.Euler(0f, 0f, -90f);
                newGravity = -transform.right;
                SmoothRotateHologram(targetRotation);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                targetRotation = transform.rotation * Quaternion.Euler(0f, 0f, 180f);
                newGravity = transform.up;
                SmoothRotateHologram(targetRotation);
            }
        }
    }

    void DestroyHologram()
    {
        isHologramVisible = false;
        Destroy(hologramInstance);
    }

    void SmoothRotateHologram(Quaternion targetRotation)
    {
        isHologramVisible = true;
        hologramInstance = Instantiate(hologramPrefab, transform.position, transform.rotation);
        hologramInstance.transform.parent = transform;

        StartCoroutine(SmoothRotate.instance.SmoothRotation(hologramInstance.transform, targetRotation, rotationSpeed));
    }
}
