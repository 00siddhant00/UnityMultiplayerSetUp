using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        SpawnPlayer(Vector3.zero);
    }

    public void SpawnPlayer(Vector3 respawnPosition)
    {
        PhotonNetwork.Instantiate(Player.name, respawnPosition, Quaternion.identity);
    }
}
