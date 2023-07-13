using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    string PLAYER_TAG = "Player";
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == PLAYER_TAG)
        {
            GameController.instance.collected++;
            Destroy(gameObject);
        }
    }
}
