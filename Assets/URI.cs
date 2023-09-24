using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using Photon.Pun;
using System;

public class URI : MonoBehaviour
{

    public Text text;
    public float URIid;
    public string parameter;
    public string idee;
    //public string parameter;
void Start()
{
    // Get the URI that launched the app
    /*
    string commandLine = System.Environment.CommandLine;
    string uriData = args[1];
    string[] splitURL = uriData.Split('/');
    string parameter = splitURL[splitURL.Length - 2];
    text.text = parameter;
    */
    /*
    string[] args = System.Environment.GetCommandLineArgs();
    if (args.Length == 1)
    {
        // Program was sent a parameter, do something with it
        string firstArg = args[1];
        int startIndex = firstArg.IndexOf("//") + 2; // Add 2 to exclude the "//" characters
        int endIndex = firstArg.IndexOf("/", startIndex);
        parameter = firstArg.Substring(startIndex, endIndex - startIndex);
        text.text = parameter;

    }
    */
    string[] args = System.Environment.GetCommandLineArgs();
    string uriData = args[1];
    //uriData = "goodrooms://5/";
    //parameter = uriData.Substring(uriData.LastIndexOf("/") - 1);
    parameter = uriData.Replace("goodrooms://", "");
    parameter = parameter.Replace("/", "");
    string[] splitString = parameter.Split('@');
    string name = splitString[1]; // "World"
    idee = splitString[0]; // "World"
    text.text = parameter;
    PhotonNetwork.NickName = name;
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "..", "LocalLow", "GoodRooms", "GoodRooms", "user.name");

    // Write the text to the file
   StreamWriter writer = new StreamWriter(path, false); // The true parameter appends to the file, false overwrites it
        writer.WriteLine(name); // Replace "This is a test" with your actual text
        writer.Close(); // Remember to close the StreamWriter
                        // Check if the parameter is a valid scene name
        if (UnityEngine.SceneManagement.SceneManager.GetSceneByName(idee) != null) {
        // Load the scene with the corresponding name
        UnityEngine.SceneManagement.SceneManager.LoadScene(idee);
    }
}


    // Update is called once per frame
    void Update()
    {
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "..", "LocalLow", "GoodRooms", "GoodRooms", "user.name");
        if (File.Exists(path))
        {
            StreamReader reader = new StreamReader(path);
            string nickName = reader.ReadLine();
            reader.Close();

            // Set the PhotonNetwork.NickName property
            PhotonNetwork.NickName = nickName;
        }
        if (!float.TryParse(idee, out URIid))
    {
        // The string is a valid float, and myFloat now contains the float value.
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    } 
    else if (UnityEngine.SceneManagement.SceneManager.GetSceneByName(idee) != null)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Load");
    }
    }
}
