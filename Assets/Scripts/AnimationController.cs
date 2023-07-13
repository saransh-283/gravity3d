using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private bool isIdle;
    private bool isFalling;
    private bool isRunning;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check for input and set animation parameters accordingly
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        isIdle = !isRunning && !isFalling;

        // Update animator parameters
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isIdle", isIdle);
        animator.SetBool("isFalling", PlayerController.isFalling);
    }
}
