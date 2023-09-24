using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks
{   
    public PhotonView playerPrefab;
    private string parameter;
    public string Name;
    public GameObject Error;
    private bool connected = false;
    public Text errortext;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
        connected = true;
       // RoomOptions roomOptions = new RoomOptions();
        //roomOptions.MaxPlayers = 4; // set maximum number of players
        Debug.Log("Connected to Master");
        /*if (!PhotonNetwork.JoinOrCreateRoom("TestForNow", roomOptions, null))
        {
            Debug.LogError("Failed to join or create room: TestForNow");
        }
        if (!PhotonNetwork.JoinRoom("Test Room"))
        {
            // if the room doesn't exist, create a new room with the specified name
            /*
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 4; // set the maximum number of players allowed in the room
            PhotonNetwork.CreateRoom("Test Room", options);
            */
            /*
            CreateRoom("Test Room");
        }
        */
        //string[] args = System.Environment.GetCommandLineArgs();
        //string uriData = args[1];
        //uriData = "goodrooms://5/";
        //parameter = uriData.Substring(uriData.LastIndexOf("/") - 1);
        //parameter = uriData.Replace("goodrooms://", "");
        //parameter = parameter.Replace("/", "");
        //string[] splitString = parameter.Split('@');
        //string name = splitString[1]; // "World"
        RoomOptions roomOptions = new RoomOptions();
        TypedLobby typedLobby = new TypedLobby(Name, LobbyType.Default);

        PhotonNetwork.JoinOrCreateRoom(Name, roomOptions, typedLobby);
    }
    public override void OnJoinedRoom() 
    {
        Debug.Log("Joined Room");
        PhotonNetwork.Instantiate(playerPrefab.name, playerPrefab.transform.position, playerPrefab.transform.rotation);
    }
    public override void OnCreatedRoom() 
    {
        Debug.Log("Created Room");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Error.SetActive(true);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Error.SetActive(true);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (!connected && cause == DisconnectCause.None)
        {
            Debug.LogError("Failed to connect to Photon.");
        }
        else
        {
            Debug.LogWarning("Disconnected from Photon: " + cause);
            errortext.gameObject.SetActive(true);
            StartCoroutine(LoadSceneAfterDelay());
        }
    }
    private System.Collections.IEnumerator LoadSceneAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(5f);

        // Load the target scene
        SceneManager.LoadScene("MainMenu");
    }
}
