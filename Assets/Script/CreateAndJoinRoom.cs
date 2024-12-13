using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    public GameObject directJoinPanel; // Panel for direct room join/create
    public GameObject roomListPanel;   // Panel for room list
    public TextMeshProUGUI CreateId;   // Input field for creating a room
    public TextMeshProUGUI JoinId;     // Input field for joining a room
    public Transform RoomListParent;  // Parent for the room list items
    public GameObject RoomListItemPrefab; // Prefab for individual room items

    void Start()
    {
        ShowDirectJoinPanel(); // Default to direct join panel
    }

    public void ShowDirectJoinPanel()
    {
        directJoinPanel.SetActive(true);
        roomListPanel.SetActive(false);
    }

    public void ShowRoomListPanel()
    {
        directJoinPanel.SetActive(false);
        roomListPanel.SetActive(true);
        PhotonNetwork.JoinLobby(); // Ensure the player joins the lobby to see rooms
    }

    public void CreateRoom()
    {
        if (!string.IsNullOrEmpty(CreateId.text))
        {
            PhotonNetwork.CreateRoom(CreateId.text, new RoomOptions { MaxPlayers = 4 });
        }
        else
        {
            Debug.LogError("Room name cannot be empty!");
        }
    }

    public void JoinRoom()
    {
        if (!string.IsNullOrEmpty(JoinId.text))
        {
            PhotonNetwork.JoinRoom(JoinId.text);
        }
        else
        {
            Debug.LogError("Room name cannot be empty!");
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform child in RoomListParent)
        {
            Destroy(child.gameObject); // Clear the existing list
        }

        foreach (RoomInfo room in roomList)
        {
            if (room.RemovedFromList) continue;

            GameObject roomItem = Instantiate(RoomListItemPrefab, RoomListParent);

            // Assign room name
            TextMeshProUGUI[] textFields = roomItem.GetComponentsInChildren<TextMeshProUGUI>();
            if (textFields.Length >= 2)
            {
                textFields[0].text = room.Name; // First TextMeshPro for room name
                textFields[1].text = $"Players: {room.PlayerCount}/{room.MaxPlayers}"; // Second TextMeshPro for player count
            }

            // Assign button functionality
            roomItem.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(() =>
            {
                PhotonNetwork.JoinRoom(room.Name);
            });
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        PhotonNetwork.LoadLevel("Play"); // Load the game scene after joining a room
    }
}
