using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothRotate : MonoBehaviour
{
    public static SmoothRotate instance;

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator SmoothRotation(Transform transform, Quaternion targetRotation, float rotationSpeed, System.Action callback = null)
    {
        Quaternion initialRotation = transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, elapsedTime);
            yield return null;
        }

        transform.rotation = targetRotation;

        callback?.Invoke();
    }
}
