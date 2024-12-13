using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;  // Photon PUN for networking
using UnityEngine.UI;
using TMPro;  // TextMeshPro for UI text elements
using UnityEngine.SceneManagement;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    #region Public Variables

    // UI elements for entering room names
    public TextMeshProUGUI CreateId;  // Input field for creating a room
    public TextMeshProUGUI JoinId;    // Input field for joining a room

    #endregion

    #region Private Variables

    // (Currently unused) 

    #endregion

    #region Events Variables

    // (Currently unused) 

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        // Initialization code can be added here if needed
    }

    // Update is called once per frame
    void Update()
    {
        // Code to run every frame can be added here
    }

    #endregion

    #region Public Methods

    // Called when a room is successfully created
    public override void OnCreatedRoom()
    {
        Debug.Log("Room Created");
    }

    // Called when the player successfully joins a room
    public override void OnJoinedRoom()
    {
        Debug.Log("Room Joined");
        PhotonNetwork.LoadLevel("Play");  // Load the game scene named "Play" after joining a room
    }

    // Called when room creation fails
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);  // Log failure and provide a reason
    }

    // Called when joining a room fails
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);  // Log failure and provide a reason
    }

    #endregion

    #region Private Methods

    // (Currently unused)

    #endregion

    #region Button Methods

    // Method called when the "Create Room" button is pressed
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(CreateId.text);  // Create a room with the name entered in the CreateId field
        Debug.Log($"{CreateId.text} Room successfully created");
    }

    // Method called when the "Join Room" button is pressed
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(JoinId.text);  // Join a room with the name entered in the JoinId field
        Debug.Log($"{JoinId.text} Room successfully created");
    }

    #endregion

}
