using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGame : MonoBehaviour
{
    public void Restart()
    {
        // Call the RestartGame method from the GameController instance
        GameController.instance.RestartGame();
    }
}
