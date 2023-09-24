using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCam : MonoBehaviour
{
    public float moveSpeed = 5f; // The speed at which the camera moves
    public float lookSpeed = 5f; // The speed at which the camera rotates
    public float minY = -90f; // The minimum Y angle of the camera
    public float maxY = 90f; // The maximum Y angle of the camera

    private bool mouseLocked = false; // Whether the mouse is locked or not

    void Update()
    {   
        if (mouseLocked == true) 
        {

        // Handle camera movement
        /*
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        */
                    float horizontal = 0f;
            if (Input.GetKey(KeyCode.A)) {
                horizontal = -1f;
            } else if (Input.GetKey(KeyCode.D)) {
                horizontal = 1f;
            }
            float vertical = 0f;
            if (Input.GetKey(KeyCode.W)) {
                vertical = 1f;
            } else if (Input.GetKey(KeyCode.S)) {
                vertical = -1f;
            }
        float up = Input.GetKey(KeyCode.E) ? 1f : 0f;
        float down = Input.GetKey(KeyCode.Q) ? -1f : 0f;
        Vector3 direction = new Vector3(horizontal, up + down, vertical).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // Handle camera rotation
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = -Input.GetAxis("Mouse Y") * lookSpeed;
        float rotationY = transform.localEulerAngles.y + mouseX;
        float rotationX = transform.localEulerAngles.x + mouseY;
        if (rotationX > 180f) {
            rotationX -= 360f;
        }
        rotationX = Mathf.Clamp(rotationX, minY, maxY);
        if (rotationX < minY || rotationX > maxY) {
            rotationY += 180f;
        }
        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0f);
        }
        // Handle mouse locking and hiding
        if (Input.GetMouseButtonDown(1)) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            mouseLocked = true;
        } else if (Input.GetMouseButtonUp(1)) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mouseLocked = false;
        }
    }
    void OnApplicationFocus(bool hasFocus) {
        // Unlock and show the mouse cursor when the application loses focus
        if (!hasFocus && mouseLocked) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mouseLocked = false;
        }
    }
}