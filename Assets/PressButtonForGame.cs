using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PressButtonForGame : MonoBehaviour
{
    public Text textthing;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.JoystickButton0))
        {
            SceneManager.LoadScene("1");
        }
        if (Input.GetKeyUp(KeyCode.JoystickButton2))
        {
            SceneManager.LoadScene("2");
        }
        if (Input.GetKeyUp(KeyCode.JoystickButton1) || Input.GetKeyUp(KeyCode.T))
        {
            Application.OpenURL("https://goodrooms.xyz/game");
        }
    }
    public void idthing()
    {
        PlayerPrefs.SetInt("JangKey", int.Parse(textthing.text));
        SceneManager.LoadScene("LoadFromPref");
    }
}
