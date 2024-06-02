using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootBoard : MonoBehaviour
{
    public float startPointZ;
    public float endPointZ;
    public float speed = 2f;

    private Vector3 nextPosition;
    private Rigidbody playerRigidbody; // 플레이어 Rigidbody

    void Start()
    {
        nextPosition = new Vector3(transform.position.x, transform.position.y, startPointZ);
    }

    void Update()
    {
        // 발판 이동 처리
        if (transform.position.z == endPointZ)
        {
            nextPosition = new Vector3(transform.position.x, transform.position.y, startPointZ);
        }
        else if (transform.position.z == startPointZ)
        {
            nextPosition = new Vector3(transform.position.x, transform.position.y, endPointZ);
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                Vector3 playerPosition = playerRigidbody.position;
                playerPosition.z = transform.position.z;
                playerRigidbody.position = playerPosition;
            }
        }
    }
}
