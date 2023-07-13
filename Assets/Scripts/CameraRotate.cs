using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float sensitivity = 5f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    private void Update()
    {
        if (!GameController.instance.isPaused)
        {
            // Get mouse movement
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            // Calculate new horizontal rotation
            rotationY += mouseX;
            rotationY = Mathf.Clamp(rotationY, -60f, 60f);

            // Calculate new vertical rotation
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -60f, 60f);

            // Rotate the camera based on mouse movement
            transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        }
    }
}
