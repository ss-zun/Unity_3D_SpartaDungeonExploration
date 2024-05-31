using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Movement movement;
    private Jump jump;
    private Look look;

    private Rigidbody rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        movement = GetComponent<Movement>();
        jump = GetComponent<Jump>();
        look = GetComponent<Look>();
    }
    private void Start()
    {
        movement.moveState = MoveState.Walking;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        movement.Move(rb);
    }

    private void LateUpdate()
    {
        if (look.canLook)
        {
            look.CameraLook();
        }
    }


    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {           
            movement.curMovementInput = context.ReadValue<Vector2>();
            if (movement.moveState == MoveState.Walking)
            {
                animator.SetBool("isRun", false);
                animator.SetBool("isWalk", true);
            }
            else if (movement.moveState == MoveState.Running)
            {
                animator.SetBool("isWalk", false);
                animator.SetBool("isRun", true);
            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            movement.curMovementInput = Vector2.zero;
            if (movement.moveState == MoveState.Walking)
            {               
                animator.SetBool("isWalk", false);
            }
            else if(movement.moveState == MoveState.Running)
            {
                animator.SetBool("isRun", false);
            }
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {     
        if (context.phase == InputActionPhase.Started && jump.IsGrounded())
        {
            animator.SetTrigger("isJump");
            rb.AddForce(Vector2.up * jump.jumpForce, ForceMode.Impulse);
        }
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        look.mouseDelta = context.ReadValue<Vector2>();
    }

    public IEnumerator SpeedUp(float duration)
    {
        movement.moveSpeed += 5;
        movement.moveState = MoveState.Running;
        yield return new WaitForSeconds(duration);
        movement.moveState = MoveState.Walking;
        movement.moveSpeed -= 5;
    }
}
