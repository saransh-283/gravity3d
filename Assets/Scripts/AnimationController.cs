using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private bool isIdle;
    private bool isFalling;
    private bool isRunning;

    // Animation parameter names
    string IS_RUNNING = "isRunning";
    string IS_IDLE = "isIdle";
    string IS_FALLING = "isFalling";

    private void Start()
    {
        // Get the Animator component from the GameObject this script is attached to
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check for input and set animation parameters accordingly
        isRunning = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        isIdle = !isRunning && !isFalling;

        // Update animator parameters
        animator.SetBool(IS_RUNNING, isRunning);
        animator.SetBool(IS_IDLE, isIdle);
        animator.SetBool(IS_FALLING, PlayerController.isFalling);
    }
}
