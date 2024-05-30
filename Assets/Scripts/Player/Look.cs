using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    public float lookSensitivity;

    [HideInInspector]
    public bool canLook = true;
    [HideInInspector]
    public Vector2 mouseDelta;

    private float camCurXRot;

    public void CameraLook()
    {
        cameraContainer.LookAt(transform.position);

        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);

        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);

        cameraContainer.position = transform.position - (cameraContainer.rotation * new Vector3(0f, 0f, 2f));
        cameraContainer.position += new Vector3(0f, 1.7f, 0f);
    }
}
