using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Movement movement;
    private Jump jump;
    private Look look;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            movement.curMovementInput = Vector2.zero;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && jump.IsGrounded())
        {
            rb.AddForce(Vector2.up * jump.jumpForce, ForceMode.Impulse);
        }
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        look.mouseDelta = context.ReadValue<Vector2>();
    }
}
