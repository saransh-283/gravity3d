using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float sensitivity = 5f; // The sensitivity of the mouse movement

    private float rotationX = 0f; // The current rotation around the X-axis
    private float rotationY = 0f; // The current rotation around the Y-axis

    float minX = -60f; // The minimum rotation limit around the X-axis
    float maxX = 60f; // The maximum rotation limit around the X-axis
    float minY = -60f; // The minimum rotation limit around the Y-axis
    float maxY = 60f; // The maximum rotation limit around the Y-axis

    private void Update()
    {
        if (!GameController.instance.isPaused) // Check if the game is not paused
        {
            // Get mouse movement
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            // Calculate new horizontal rotation
            rotationY += mouseX;
            rotationY = Mathf.Clamp(rotationY, minY, maxY);

            // Calculate new vertical rotation
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, minX, maxX);

            // Rotate the camera based on mouse movement
            transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        }
    }
}
