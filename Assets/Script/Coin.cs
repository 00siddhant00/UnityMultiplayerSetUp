using Photon.Pun;
using UnityEngine;
using ExitGames.Client.Photon; // For Hashtable

public class Coin : MonoBehaviourPunCallbacks
{
    public string coinID; // Unique ID for the coin
    public AudioClip collectSFX;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        coinID = GetComponent<PhotonView>().ViewID.ToString();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = collectSFX;
        audioSource.playOnAwake = false;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(coinID, out object isCollected) && (bool)isCollected)
        {
            gameObject.SetActive(false);
        }
    }

    [PunRPC]
    public void PlaySFX()
    {
        spriteRenderer.enabled = false;
        audioSource.Play();
        Destroy(gameObject, collectSFX.length);
    }

    [PunRPC]
    public void MarkAsCollected()
    {
        Hashtable coinState = PhotonNetwork.CurrentRoom.CustomProperties;
        coinState[coinID] = true;
        print(coinID);
        PhotonNetwork.CurrentRoom.SetCustomProperties(coinState);
    }

    [PunRPC]
    public void RequestMarkAsCollected(int coinViewID)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // Find the coin by its PhotonView ID
            PhotonView coinView = PhotonView.Find(coinViewID);
            if (coinView != null)
            {
                Coin coinScript = coinView.GetComponent<Coin>();
                if (coinScript != null)
                {
                    // Mark coin as collected and sync the state across the network
                    coinScript.MarkAsCollected();
                    // Set the coin as collected in the room's custom properties (or any other way of syncing)
                    Hashtable coinState = PhotonNetwork.CurrentRoom.CustomProperties;
                    coinState[coinScript.coinID] = true; // Mark this coin as collected
                    PhotonNetwork.CurrentRoom.SetCustomProperties(coinState);
                }
                else
                {
                    Debug.LogError("Coin script not found on the coin object.");
                }
            }
            else
            {
                Debug.LogError("PhotonView not found with ID: " + coinViewID);
            }
        }
    }
}
