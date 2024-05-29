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
            animator.SetBool("isWalk", true);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            movement.curMovementInput = Vector2.zero;
            animator.SetBool("isWalk", false);
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
}
