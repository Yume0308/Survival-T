using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public Camera camera;

    public float mouseSensitivity = 100.0f;

    float xRotation = 0f, yRotation = 0f;


    // Start is called before the first frame update
    void Start()
    {
        // looking the cursor to the middle of the screen and making it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float xMouse = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float yMouse = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Control the rotation in x-axis(looking up and down)
        xRotation -= yMouse;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Control the rotation in y-axis(looking left and right)
        yRotation += xMouse;

        // Rotate the camera
        camera.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
