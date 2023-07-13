using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramRotate : MonoBehaviour
{
    public float rotationSpeed = 2f;
    public GameObject hologramPrefab;
    public static GameObject hologramInstance;

    static bool isHologramVisible = false;

    void Update()
    {
        if (!isHologramVisible && !GameController.instance.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                RotateRight();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                RotateLeft();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                RotateTop();
            }
        }
    }

    void RotateRight()
    {
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0f, 0f, 90f);
        SmoothRotation(targetRotation);
    }

    void RotateLeft()
    {
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0f, 0f, -90f);
        SmoothRotation(targetRotation);
    }

    void RotateTop()
    {
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0f, 0f, 180f);
        SmoothRotation(targetRotation);
    }

    public static void DestroyHologram()
    {
        isHologramVisible = false;
        Destroy(hologramInstance);
    }

    void SmoothRotation(Quaternion targetRotation)
    {
        isHologramVisible = true;
        hologramInstance = Instantiate(hologramPrefab, transform.position, transform.rotation);
        hologramInstance.transform.parent = transform;

        StartCoroutine(SmoothRotate.instance.SmoothRotation(hologramInstance.transform, targetRotation, rotationSpeed));
    }
}
