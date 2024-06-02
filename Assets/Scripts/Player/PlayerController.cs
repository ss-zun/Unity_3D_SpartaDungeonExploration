using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController : MonoBehaviour
{
    public Movement movement;
    private Jump jump;
    private Look look;
    public Climb climb;

    private Rigidbody rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        movement = GetComponent<Movement>();
        jump = GetComponent<Jump>();
        look = GetComponent<Look>();
        climb = GetComponent<Climb>();
    }
    private void Start()
    {
        movement.moveState = MoveState.Walking;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        if (movement.moveState == MoveState.Climbing)
        {
            climb.ClimbRope(movement.curMovementInput.y, rb);
        }
        else
        {
            movement.Move(rb);
        }
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
        if (context.phase == InputActionPhase.Performed || context.phase == InputActionPhase.Canceled)
        {
            movement.curMovementInput = context.ReadValue<Vector2>();
            switch (movement.moveState)
            {
                case MoveState.Walking:
                    animator.SetBool("isRun", false);
                    animator.SetBool("isClimb", false);
                    animator.SetBool("isWalk", context.phase == InputActionPhase.Performed);
                    break;
                case MoveState.Running:
                    animator.SetBool("isWalk", false);
                    animator.SetBool("isClimb", false);
                    animator.SetBool("isRun", context.phase == InputActionPhase.Performed);
                    break;
                case MoveState.Climbing:
                    animator.SetBool("isWalk", false);
                    animator.SetBool("isRun", false);
                    animator.SetBool("isClimb", true);
                    rb.useGravity = false;
                    rb.velocity = Vector3.zero;
                    break;
            }
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {     
        if (context.phase == InputActionPhase.Started && jump.IsGrounded())
        {
            animator.SetTrigger("isJump");
            rb.AddForce(Vector2.up * jump.jumpForce, ForceMode.Impulse);
            transform.GetComponent<PlayerCondition>().UseStamina();
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
