using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Experimental.GlobalIllumination;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject Player;
    public GameObject Coin;
    public Transform[] CoinsSpawn;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        SpawnPlayer(Vector3.zero);
        SpawnCoin();
    }

    public void SpawnPlayer(Vector3 respawnPosition)
    {
        PhotonNetwork.Instantiate(Player.name, respawnPosition, Quaternion.identity);
    }

    public void SpawnCoin()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < CoinsSpawn.Length; i++)
            {
                PhotonNetwork.Instantiate(Coin.name, CoinsSpawn[i].transform.position, Quaternion.identity);
            }
        }
    }
}
