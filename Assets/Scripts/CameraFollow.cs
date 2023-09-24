using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraFollow : MonoBehaviour {

    public float CameraMoveSpeed = 120.0f;
    public GameObject CameraFollowObj;
    Vector3 followPOS;
    public float clampAngle = 80.0f;
    public float inputSensitivity = 150.0f;
    public GameObject CameraObj;
    public GameObject PlayerObj;
    public float camDistanceXToPlayer;
    public float camDistanceYToPlayer;
    public float camDistanceZToPlayer;
    public float mouseX;
    public float mouseY;
    public float finalInputX;
    public float finalInputZ;
    public float smoothX;
    public float smoothY;
    private float rotY = 0.0f;
    private float rotX = 0.0f;
    public GameObject prefabToSearch;


    // Start is called before the first frame update
    void Start() {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
private void Awake() {
    // CameraFollowObj = gameObject.transform.parent.Find("Player").Find("CameraFollow").gameObject;
}
    // Update is called once per frame
void Update() {
    if (Input.GetMouseButton(1) || IsControllerInput()) {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        finalInputX = Input.GetAxis("Right Stick X") + mouseX;
        finalInputZ = Input.GetAxis("Right Stick Y") + mouseY;

        rotY += finalInputX * inputSensitivity * Time.deltaTime;
        rotX -= finalInputZ * inputSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    } else {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

}
    // Function to check if controller input is detected
    bool IsControllerInput()
    {
        // Check right stick input
        float rightStickX = Input.GetAxis("Right Stick X");
        float rightStickY = Input.GetAxis("Right Stick Y");
        bool hasRightStickInput = Mathf.Abs(rightStickX) > 0.1f || Mathf.Abs(rightStickY) > 0.1f;

        // Check if any joystick button is pressed
        bool hasJoystickButtonInput = false;
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKey("joystick 1 button " + i))
            {
                hasJoystickButtonInput = true;
                break;
            }
        }

        // Return true if any right stick input is detected and no mouse input is detected
        return (hasRightStickInput || hasJoystickButtonInput);
    }


    void LateUpdate() {
        CameraUpdater();
    }

    void CameraUpdater() {

        Transform target = CameraFollowObj.transform;


        float step = CameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
    public Quaternion PlanarRotation => Quaternion.Euler(0, rotY, 0);
}
