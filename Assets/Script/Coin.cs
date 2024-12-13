using Photon.Pun;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip collectSFX;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = collectSFX;
        audioSource.playOnAwake = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    [PunRPC]
    public void PlaySFX()
    {
        spriteRenderer.enabled = false;
        audioSource.Play();
        Destroy(gameObject, collectSFX.length);
    }
}
