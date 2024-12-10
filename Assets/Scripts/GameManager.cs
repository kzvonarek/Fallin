using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // floor speed for FloorMovement.cs
    [HideInInspector] public float vertObjSpeed;
    [SerializeField] float objSpeedInc;
    [SerializeField] float maxObjSpeed;

    // current time functionality
    [SerializeField] TextMeshProUGUI currentTimeText;
    [SerializeField] TextMeshProUGUI outlineCurrentTimeText;
    private int currentTime;
    private float timeIncTimer;

    // best time functionality
    [SerializeField] TextMeshProUGUI finalTimeText;
    [SerializeField] TextMeshProUGUI bestTimeText;

    // pause functionality
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject pauseMenu;

    // death functionality
    [SerializeField] GameObject deathMenu;

    void Start()
    {
        Time.timeScale = 1;

        timeIncTimer = 0f;
        currentTime = 0;
        bestTimeUpdate();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game Play")
        {
            incTime();

            // floor speed increasing until it is greater than max floor speed [FloorMovement.cs]
            if (vertObjSpeed < maxObjSpeed)
            {
                vertObjSpeed += objSpeedInc * Time.deltaTime;
            }
        }
    }

    public void pauseGame()
    {
        pauseMenu.SetActive(true); // make pause menu visible
        pauseButton.SetActive(false); // make pause button invisible
        Time.timeScale = 0; // freeze game
    }

    public void unpauseGame()
    {
        pauseMenu.SetActive(false); // make pause menu invisible
        pauseButton.SetActive(true); // make pause button visible
        Time.timeScale = 1; // unfreeze game
    }

    // on death hide all icons/UI, and allow for Main Menu or game Restart
    public void death()
    {
        // gMaudioSource.PlayOneShot(gameOverSFX, 0.5f); // play death sound

        Time.timeScale = 0; // freeze game

        bestTimeUpdate(); // update final and best time

        deathMenu.SetActive(true); // enable pause menu
        pauseButton.SetActive(false); // disable pause button
    }

    private void incTime()
    {
        // time increases by 1 every second alive
        timeIncTimer += Time.deltaTime;
        if (timeIncTimer >= 1f) // inc each second
        {
            currentTime += 1;
            currentTimeText.text = currentTime.ToString(); // update UI text
            outlineCurrentTimeText.text = currentTime.ToString(); // update UI text outline
            timeIncTimer = 0f; // reset timer
        }
    }

    private void bestTimeUpdate()
    {
        // check if best time exists
        if (PlayerPrefs.HasKey("Saved Best Time"))
        {
            // check if new time is higher than best time
            if (currentTime > PlayerPrefs.GetInt("Saved Best Time"))
            {
                PlayerPrefs.SetInt("Saved Best Time", currentTime);
            }
        }
        else
        {
            // if there is no best time
            PlayerPrefs.SetInt("Saved Best Time", currentTime);
        }

        // update associated texts
        if (SceneManager.GetActiveScene().name == "Game Play")
        {
            finalTimeText.text = "Time: " + currentTime.ToString();
        }
        bestTimeText.text = "Best Time: " + PlayerPrefs.GetInt("Saved Best Time").ToString();
    }
}