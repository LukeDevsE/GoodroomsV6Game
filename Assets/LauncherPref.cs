using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LauncherPref : MonoBehaviourPunCallbacks
{   
    public PhotonView playerPrefab;
    private string parameter;
    public GameObject Error;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
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
        TypedLobby typedLobby = new TypedLobby(PlayerPrefs.GetInt("JangKey").ToString(), LobbyType.Default);

        PhotonNetwork.JoinOrCreateRoom(PlayerPrefs.GetInt("JangKey").ToString(), roomOptions, typedLobby);
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
}
