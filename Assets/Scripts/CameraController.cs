using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerBody;
    public float mouseSensitivity;

    private float xAxisClamp = 0.0f;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void Update()
    {
        // Show cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        this.RotateCamera();
    }

    private void RotateCamera()
    {
        float MouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float MouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        this.xAxisClamp -= MouseY;

        Vector3 targetRotCam = transform.rotation.eulerAngles;
        Vector3 targetRotBody = playerBody.rotation.eulerAngles;

        targetRotCam.x -= MouseY;
        targetRotCam.z = 0;
        targetRotBody.y += MouseX;

        if (this.xAxisClamp > 90)
        {
            this.xAxisClamp = 90;
            targetRotCam.x = 90;
        }
        else if (this.xAxisClamp < -60)
        {
            this.xAxisClamp = -60;
            targetRotCam.x = 300;
        }

        this.transform.rotation = Quaternion.Euler(targetRotCam);
        this.playerBody.rotation = Quaternion.Euler(targetRotBody);
    }
}
