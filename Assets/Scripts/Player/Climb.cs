using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{
    private GameObject ropeSegment;
    private float ropeHeight;

    public void SetRopeSegment(GameObject rope)
    {
        ropeSegment = rope;
        ropeHeight = ropeSegment.GetComponent<Renderer>().bounds.size.y;
    }
    public void ClimbRope(float direction, Rigidbody rb)
    {
        float climbSpeed = 1.0f;
        if (direction != 0)
        {
            Vector3 climbDirection = new Vector3(0, direction * climbSpeed * Time.deltaTime, 0);
            rb.MovePosition(transform.position + climbDirection);

            if (transform.position.y >= ropeSegment.transform.position.y + (ropeHeight / 2))
            {
                transform.position = new Vector3(transform.position.x, ropeSegment.transform.position.y + (ropeHeight / 2), transform.position.z);
                gameObject.GetComponent<Movement>().moveState = MoveState.Walking;
                rb.useGravity = true;
            }
        }
    }
}
