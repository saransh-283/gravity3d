using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothRotate : MonoBehaviour
{
    public static SmoothRotate instance;

    private void Awake()
    {
        // Set the instance to this script for easy access from other scripts
        instance = this;
    }

    public IEnumerator SmoothRotation(Transform transform, Quaternion targetRotation, float rotationSpeed, System.Action callback = null)
    {
        // Store the initial rotation of the transform
        Quaternion initialRotation = transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            // Increase the elapsed time based on delta time and rotation speed
            elapsedTime += Time.deltaTime * rotationSpeed;

            // Interpolate between the initial rotation and target rotation using Lerp
            transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, elapsedTime);

            // Wait for the next frame
            yield return null;
        }

        // Set the final rotation to the target rotation
        transform.rotation = targetRotation;

        // Invoke the callback function if provided
        callback?.Invoke();
    }
}
