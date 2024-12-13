using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using ExitGames.Client.Photon; // For Hashtable

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;

    private Vector2 movement;
    private bool isGrounded;
    private PhotonView view;

    private void Start()
    {
        groundCheck = transform.GetChild(0);
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (view.IsMine)
        {
            movement.x = Input.GetAxis("Horizontal");

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (view.IsMine && collision.CompareTag("Coin"))
        {
            PhotonView coinView = collision.GetComponent<PhotonView>();
            if (coinView != null)
            {
                coinView.RPC("PlaySFX", RpcTarget.All);

                Coin coinScript = collision.GetComponent<Coin>();

                // Request the Master Client to mark the coin as collected
                if (!PhotonNetwork.IsMasterClient)
                {
                    // Pass the PhotonView ID to the Master Client
                    coinView.RPC("RequestMarkAsCollected", RpcTarget.MasterClient, coinView.ViewID);
                }
                else
                {
                    // If this client is the Master Client, mark directly
                    if (coinScript != null)
                    {
                        coinScript.MarkAsCollected();
                    }
                }
            }
        }
    }
}
