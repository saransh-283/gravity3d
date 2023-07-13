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
        StartCoroutine(SmoothRotate(targetRotation));
    }

    void RotateLeft()
    {
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0f, 0f, -90f);
        StartCoroutine(SmoothRotate(targetRotation));
    }

    void RotateTop()
    {
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0f, 0f, 180f);
        StartCoroutine(SmoothRotate(targetRotation));
    }

    public static void DestroyHologram()
    {
        isHologramVisible = false;
        Destroy(hologramInstance);
    }

    IEnumerator SmoothRotate(Quaternion targetRotation)
    {
        isHologramVisible = true;
        hologramInstance = Instantiate(hologramPrefab, transform.position, transform.rotation);
        hologramInstance.transform.parent = transform;
        Quaternion initialRotation = hologramInstance.transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * rotationSpeed;
            hologramInstance.transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, elapsedTime);
            yield return null;
        }

        hologramInstance.transform.rotation = targetRotation;
    }
}
