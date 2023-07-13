using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    
    public int collected = 0;
    public float G = 50f;
    public bool isPaused = false;
    public float timer = 120; // In seconds
    public int gameStatus = 0; // 0 = playing, 1 = success, -1 = failed
    public int numberOfCollectibles = 10;

    public TextMeshProUGUI timeComponent;
    public TextMeshProUGUI collectedComponent;

    public GameObject instructionsUI;
    public GameObject successUi;
    public GameObject failedUi;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        Physics.gravity = Vector3.down * G;
    }
    // Update is called once per frame
    void Update()
    {
        if (collected == numberOfCollectibles && timer > 0)
        {
            Time.timeScale = 0;
            isPaused = true;
            successUi.SetActive(true);
        }
        else if (timer <= 0)
        {
            Time.timeScale = 0;
            isPaused = true;
            failedUi.SetActive(true);
        }
        else
        {
            collectedComponent.text = $"Collected: {collected}/10";
            CountDown();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = isPaused ? 1 : 0;
                isPaused = !isPaused;
                instructionsUI.SetActive(!instructionsUI.activeSelf);
            }
        }
    }

    void CountDown()
    {
        int currentMinutes = Mathf.FloorToInt(timer / 60);
        int currentSeconds = Mathf.FloorToInt(timer % 60);

        timer -= Time.deltaTime;
        string time = string.Format("{0:00}:{1:00}", currentMinutes, currentSeconds);
        timeComponent.text = time;
    }

    public void RestartGame()
    {
        print("Restarting game...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Time.timeScale = 1;
        isPaused = false;

        collected = 0;
        timer = 10;
        gameStatus = 0;

    }
}
