using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class XboxNumpadInput : MonoBehaviour
{
    public InputField inputField;

    private TouchScreenKeyboard keyboard;
    private bool isNumpadOpen = false;

    private void Update()
    {
        // Check for Y button press on the Xbox controller
        if (!isNumpadOpen && Input.GetKeyUp(KeyCode.JoystickButton3))
        {
            // Open the numpad keyboard
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NumberPad);
            isNumpadOpen = true;
        }

        // Check if the numpad keyboard is active
        if (keyboard != null && keyboard.active)
        {
            // Update the input field text with the numpad keyboard input
            inputField.text = keyboard.text;
        }

        // Check if the numpad keyboard should be closed (e.g., when pressing Enter/Return key)
        if (keyboard.status == TouchScreenKeyboard.Status.Done || keyboard.status == TouchScreenKeyboard.Status.Visible && Input.GetKeyUp(KeyCode.Return))
        {
            // Perform actions with the input (e.g., teleporting and setting player prefs)
            TeleportAndSetPlayerPref();

            // Close the numpad keyboard
            keyboard = null;
            isNumpadOpen = false;
        }
    }

    private void TeleportAndSetPlayerPref()
    {
        // Get the value from the input field
        string inputValue = inputField.text;

        // Perform your teleportation logic here using the value from the input field

        // Set the player preference
        PlayerPrefs.SetInt("JangKey", int.Parse(inputValue));
        PlayerPrefs.Save();

        // Example: Load a new scene
        SceneManager.LoadScene("LoadFromPref");
    }
}
