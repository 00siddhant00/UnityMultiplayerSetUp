using UnityEngine;
using Photon.Pun;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    //Connets to master
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master");
    }

    // joins the lobby
    public void JoinLobby()
    {
        PhotonNetwork.JoinLobby();
    }

    //loads the lobby
    public override void OnJoinedLobby()
    {
        Debug.Log("Lobby joined");
        PhotonNetwork.LoadLevel(sceneName);
    }
}