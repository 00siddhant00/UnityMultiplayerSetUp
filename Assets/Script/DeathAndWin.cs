using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAndWin : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Example condition for "death"
            //transform.parent.GetComponent<PlayerMovement>().RespawnPlayer();
            //transform.parent.GetComponent<PlayerMovement>().Loose();
        }
    }
}
