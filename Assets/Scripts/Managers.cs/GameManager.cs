using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Android;

public class GameManager : MonoBehaviour
{
    // floor speed for FloorMovement.cs
    public float vertObjSpeed;
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
    [SerializeField] TextMeshProUGUI bestTimeTextOutline;

    // pause functionality
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject pauseMenu;
    private bool paused;

    // death functionality
    [SerializeField] GameObject deathMenu;
    [HideInInspector] public bool isDead;

    // currency (coins/gems) functionality
    // [HideInInspector] public int collectedCoins;
    // [HideInInspector] public int totalCoins;
    // [SerializeField] TextMeshProUGUI collectedCoinsText;
    // [SerializeField] TextMeshProUGUI totalCoinsText;

    void Start()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;

        timeIncTimer = 0f;
        currentTime = 0;
        isDead = false;
        bestTimeUpdate();

        // collectedCoins = 0;
        // // load total coins from PlayerPrefs
        // if (PlayerPrefs.HasKey("Total Coins"))
        // {
        //     totalCoins = PlayerPrefs.GetInt("Total Coins");
        // }
        // else
        // {
        //     totalCoins = 0; // default value
        // }
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

            // pause game when player presses escape or presses pause button
            if (Input.GetKeyDown(KeyCode.Escape) && paused == false && isDead == false)
            {
                pauseGame();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && paused == true && isDead == false) // unpause game
            {
                unpauseGame();
            }
        }

        // totalCoinsUpdate();
    }

    public void pauseGame()
    {
        paused = true;
        pauseMenu.SetActive(true); // make pause menu visible
        pauseButton.SetActive(false); // make pause button invisible
        Time.timeScale = 0; // freeze game
        bestTimeUpdate(); // update final and best time
    }

    public void unpauseGame()
    {
        paused = false;
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
        isDead = true;
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
                PlayerPrefs.Save();
            }
        }
        else
        {
            // if there is no best time
            PlayerPrefs.SetInt("Saved Best Time", currentTime);
            PlayerPrefs.Save();
        }

        // update associated texts
        if (SceneManager.GetActiveScene().name == "Game Play")
        {
            finalTimeText.text = "Time: " + currentTime.ToString();
        }
        bestTimeText.text = "Best Time: " + PlayerPrefs.GetInt("Saved Best Time").ToString();
        bestTimeTextOutline.text = "Best Time: " + PlayerPrefs.GetInt("Saved Best Time").ToString();
    }

    // private void totalCoinsUpdate()
    // {
    //     // update associated texts
    //     if (SceneManager.GetActiveScene().name == "Game Play")
    //     {
    //         collectedCoinsText.text = "x" + collectedCoins.ToString();
    //     }
    //     if (SceneManager.GetActiveScene().name == "Main Menu")
    //     {
    //         totalCoinsText.text = "Total Coins: " + PlayerPrefs.GetInt("Total Coins").ToString();
    //     }
    // }
}