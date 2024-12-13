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
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = collectSFX;
        audioSource.playOnAwake = false;
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Check if the coin has already been collected
        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(coinID, out object isCollected) && (bool)isCollected)
        {
            gameObject.SetActive(false); // Disable the coin
        }
    }

    [PunRPC]
    public void PlaySFX()
    {
        spriteRenderer.enabled = false;
        audioSource.Play();
        Destroy(gameObject, collectSFX.length);
    }

    public void MarkAsCollected()
    {
        // Update room properties to mark the coin as collected
        Hashtable coinState = PhotonNetwork.CurrentRoom.CustomProperties;
        coinState[coinID] = true; // Set coinID to collected
        PhotonNetwork.CurrentRoom.SetCustomProperties(coinState);
    }
}
