using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 850f;
    [SerializeField] float jumpForce = 8f;

    Quaternion targetRotation;

    CameraFollow cameraController;

    Rigidbody rb; // reference to Rigidbody component
    public bool isGrounded = false;
    private string groundTag = "Ground"; // replace with your desired tag
    public float groundDistance = 0.1f; // adjust this value to match the height of your player
    public bool rightGrounded;
    public bool leftGrounded;
    RaycastHit leftHit;
    //float previousMoveAmount = 0f;
    RaycastHit rightHit;
    Collider characterCollider;
    //private RealtimeView _realtimeView;
    
    private void Awake()
    {
        cameraController = Camera.main.transform.parent.gameObject.GetComponent<CameraFollow>();
        rb = GetComponent<Rigidbody>(); // get reference to Rigidbody component\
        //photonView = transform.parent.gameObject.GetComponent<photonView>();
        characterCollider = GetComponent<Collider>();
    }
    
        private void Start() {
            // Call LocalStart() only if this instance is owned by the local client
            if (photonView.IsMine)
                //LocalStart();
            if (!photonView.IsMine) {
                transform.parent.Find("CameraBase").gameObject.SetActive(false);
            }
            
        }
        /*
    private void LocalStart() {
        // Request ownership of the Player and the character RealtimeTransforms
        GetComponent<RealtimeTransform>().RequestOwnership();
        GetComponent<RealtimeView>().RequestOwnership();
    }
    */
    private void FixedUpdate() {
        // Call LocalFixedUpdate() only if this instance is owned by the local client
        if (photonView.IsMine)
            LocalFixedUpdate();
    }
    private void Update() {
        // Call LocalFixedUpdate() only if this instance is owned by the local client
        if (!photonView.IsMine) {
            transform.parent.Find("CameraBase").gameObject.SetActive(false);
            this.enabled = false;
        }
    }
    private void LocalFixedUpdate() // use FixedUpdate for physics-related updates
    {
 
        cameraController = Camera.main.transform.parent.gameObject.GetComponent<CameraFollow>();
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        //Debug.Log(h);
        //Debug.Log(v);

        // Check for keyboard input

        //float moveAmount = Mathf.Abs(h) + Mathf.Abs(v);

        var moveInput = (new Vector3(h,0,v)).normalized;

        var moveDir = cameraController.PlanarRotation * moveInput;
        //moveAmount >= previousMoveAmount && moveDir != Vector3.zero && moveAmount != 0
        if (moveDir != Vector3.zero) 
        {
            // move the Rigidbody component instead of the Transform component
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
            //rb.AddForce(moveDir * moveSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            targetRotation = Quaternion.LookRotation(moveDir);
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
            //transform.parent.gameObject.GetComponent<Animator>().enabled = true;
            transform.parent.gameObject.GetComponent<Animator>().SetBool("Walking", true);
            transform.parent.gameObject.GetComponent<Animator>().SetBool("Idle", false);
        }
        else {
          //transform.parent.gameObject.GetComponent<Animator>().enabled = true;
            transform.parent.gameObject.GetComponent<Animator>().SetBool("Walking", false);
            transform.parent.gameObject.GetComponent<Animator>().SetBool("Idle", true);
          //transform.parent.gameObject.GetComponent<Animator>().enabled = false;
          // Set X and Z components to zero
          //Vector3 blankVelocity = rb.velocity;
          //blankVelocity.x = 0f;
          //blankVelocity.z = 0f;
          // Set the Rigidbody velocity to the modified velocity vector
          //rb.velocity = blankVelocity;
        }
        
        //previousMoveAmount = moveAmount; // update the previous move amount
        // Jumping code
        //Mathf.Abs(rb.velocity.y) < 0.2f && 
        if (h + v == 0) {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
        if (leftGrounded || rightGrounded) {
            transform.parent.gameObject.GetComponent<Animator>().SetBool("Jump", false);
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.JoystickButton0))
            {
                transform.parent.gameObject.GetComponent<Animator>().SetBool("Jump", true);
                transform.parent.gameObject.GetComponent<Animator>().SetBool("Idle", false);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                leftGrounded = false;
                rightGrounded = false;
            }
        }
    }

}