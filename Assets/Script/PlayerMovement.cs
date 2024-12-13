using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

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
            }
        }
    }
}
