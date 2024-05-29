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
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        cameraContainer.LookAt(transform.position + Vector3.up * 1.5f); // �÷��̾��� �Ӹ� ��

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }
}
