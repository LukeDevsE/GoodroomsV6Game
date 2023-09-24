using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class ButtonToLeave : MonoBehaviour
{
    public GameObject Menu;

    // Update is called once per frame
    void Update()
    {
        if (Menu.active == true && Input.GetKeyUp(KeyCode.JoystickButton0))
        {
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene("MainMenu");
        }
        if (Input.GetKeyUp(KeyCode.JoystickButton2) || Input.GetKeyUp(KeyCode.JoystickButton1))
        {
            Menu.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.JoystickButton7))
        {
            Menu.SetActive(!Menu.active);
        }
    }
}
