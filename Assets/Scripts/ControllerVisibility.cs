using UnityEngine;

public class ControllerVisibility : MonoBehaviour
{
    public GameObject[] objectsToControl;
    public GameObject[] noobjectsToControl;
    void Start()
    {
        // Check if a controller is connected
        bool controllerConnected = CheckForController();

        // Set the initial visibility of the objects based on the controller connection status
        SetObjectsVisibility(controllerConnected);
    }

    void Update()
    {
        // Check for changes in the controller connection status
        bool controllerConnected = CheckForController();

        // Update the visibility of the objects based on the controller connection status
        SetObjectsVisibility(controllerConnected);
    }

    void SetObjectsVisibility(bool visible)
    {
        foreach (GameObject obj in objectsToControl)
        {
            obj.SetActive(visible);
        }
        foreach (GameObject obj in noobjectsToControl)
        {
            obj.SetActive(!visible);
        }
    }

    bool CheckForController()
    {
        // Replace "GetControllerCount()" with your own method to detect controllers based on your setup
        // For example, you could use Input.GetJoystickNames() to check if there are connected joysticks or controllers.
        string[] controllerCount = Input.GetJoystickNames();
        return controllerCount.Length > 0 && !string.IsNullOrEmpty(controllerCount[0]);
    }
}
