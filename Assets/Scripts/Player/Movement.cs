using UnityEngine;
using UnityEngine.InputSystem;

public enum MoveState
{
    Walking,
    Running,
    Climbing
}

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public MoveState moveState;

    [HideInInspector]
    public Vector2 curMovementInput;

    public void Move(Rigidbody rb)
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = rb.velocity.y;

        rb.velocity = dir;
    }
}