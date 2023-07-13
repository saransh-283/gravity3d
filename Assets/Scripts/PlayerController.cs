using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float jumpForce = 10f;
    public float turnSpeed = 2f;

    Quaternion right = Quaternion.Euler(0f, 90f, 0f);
    Quaternion left = Quaternion.Euler(0f, -90f, 0f);
    Quaternion behind = Quaternion.Euler(0f, 180f, 0f);

    public static bool isFalling = false;

    private Rigidbody myBody;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!GameController.instance.isPaused)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                Quaternion targetRotation = transform.rotation * left;
                StartCoroutine(SmoothRotate.instance.SmoothRotation(transform, targetRotation, turnSpeed));
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Quaternion targetRotation = transform.rotation * right;
                StartCoroutine(SmoothRotate.instance.SmoothRotation(transform, targetRotation, turnSpeed));
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Quaternion targetRotation = transform.rotation * behind;
                StartCoroutine(SmoothRotate.instance.SmoothRotation(transform, targetRotation, turnSpeed));
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!isFalling)
                {
                    isFalling = true;
                    myBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isFalling = false;
        }
    }
}
