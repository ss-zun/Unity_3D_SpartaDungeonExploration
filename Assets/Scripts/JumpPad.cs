using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpPadForce;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 forceDirection = Vector3.up * jumpPadForce;
            collision.rigidbody.AddForce(forceDirection, ForceMode.Impulse);
        }
    }
}
