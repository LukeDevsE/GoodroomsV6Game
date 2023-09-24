using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerOlder : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 850f;
    [SerializeField] float jumpForce = 8f;

    Quaternion targetRotation;

    CameraFollow cameraController;

    Rigidbody rb; // reference to Rigidbody component

    float previousMoveAmount = 0f;

    private void Awake()
    {
        cameraController = Camera.main.transform.parent.gameObject.GetComponent<CameraFollow>();
        rb = GetComponent<Rigidbody>(); // get reference to Rigidbody component
    }

    private void FixedUpdate() // use FixedUpdate for physics-related updates
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float moveAmount = Mathf.Abs(h) + Mathf.Abs(v);

        var moveInput = (new Vector3(h,0,v)).normalized;

        var moveDir = cameraController.PlanarRotation * moveInput;

        if (moveAmount >= previousMoveAmount && moveDir != Vector3.zero) 
        {
            // move the Rigidbody component instead of the Transform component
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
            targetRotation = Quaternion.LookRotation(moveDir);
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
        previousMoveAmount = moveAmount; // update the previous move amount
        // Jumping code
        if (Mathf.Abs(rb.velocity.y) < 0.1f && Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
