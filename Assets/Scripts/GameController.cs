using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public int collected = 0; // The number of collectibles the player has collected
    public float G = 50f; // The gravitational force applied to the game objects
    public bool isPaused = false; // Indicates whether the game is paused or not
    public float timer = 120; // The remaining time in seconds
    public int gameStatus = 0; // 0 = playing, 1 = success, -1 = failed
    public int numberOfCollectibles = 10; // The total number of collectibles in the game

    public TextMeshProUGUI timeComponent; // The UI component displaying the remaining time
    public TextMeshProUGUI collectedComponent; // The UI component displaying the number of collected collectibles

    public GameObject instructionsUI; // The UI panel for displaying instructions
    public GameObject successUi; // The UI panel for displaying the success message
    public GameObject failedUi; // The UI panel for displaying the failure message

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        Physics.gravity = Vector3.down * G; // Set the gravity based on the specified force
    }

    void Update()
    {
        if (collected == numberOfCollectibles && timer > 0)
        {
            Time.timeScale = 0; // Pause the game
            isPaused = true;
            successUi.SetActive(true); // Display the success UI panel
        }
        else if (timer <= 0)
        {
            Time.timeScale = 0; // Pause the game
            isPaused = true;
            failedUi.SetActive(true); // Display the failure UI panel
        }
        else
        {
            collectedComponent.text = $"Collected: {collected}/{numberOfCollectibles}"; // Update the collected UI component with the current collected count
            CountDown();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = isPaused ? 1 : 0; // Toggle the game's time scale to pause or resume the game
                isPaused = !isPaused;
                instructionsUI.SetActive(!instructionsUI.activeSelf); // Toggle the visibility of the instructions UI panel
            }
        }
    }

    void CountDown()
    {
        int currentMinutes = Mathf.FloorToInt(timer / 60); // Calculate the current remaining minutes
        int currentSeconds = Mathf.FloorToInt(timer % 60); // Calculate the current remaining seconds

        timer -= Time.deltaTime; // Reduce the remaining time based on the time passed in the current frame
        string time = string.Format("{0:00}:{1:00}", currentMinutes, currentSeconds); // Format the time string in "mm:ss" format
        timeComponent.text = time; // Update the time UI component with the current remaining time
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene to restart the game

        Time.timeScale = 1; // Reset the time scale to normal
        isPaused = false; // Set the game to not paused

        collected = 0; // Reset the collected count to 0
        timer = 10; // Reset the timer to the initial value
        gameStatus = 0; // Reset the game status to playing
    }
}
