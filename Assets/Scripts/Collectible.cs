using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    string PLAYER_TAG = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == PLAYER_TAG)
        {
            // Increase the collected count in the GameController instance
            GameController.instance.collected++;

            // Destroy the collectible object
            Destroy(gameObject);
        }
    }
}
