using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // floor speed for FloorMovement.cs
    [HideInInspector] public float vertObjSpeed;
    [SerializeField] float objSpeedInc;
    [SerializeField] float maxObjSpeed;

    // score functionality
    [SerializeField] TextMeshProUGUI inGameScoreText;
    [SerializeField] TextMeshProUGUI outlineScoreText;
    private int score;
    private float scoreIncTimer = 0f;

    // pause functionality
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject pauseMenu;

    // death functionality
    [SerializeField] GameObject deathMenu;

    void Start()
    {
        Time.timeScale = 1;
        score = 0;
    }

    void Update()
    {
        // score increases by 1 every second alive
        scoreIncTimer += Time.deltaTime;
        if (scoreIncTimer >= 1f) // inc each second
        {
            score += 1;
            inGameScoreText.text = score.ToString(); // update UI text
            outlineScoreText.text = score.ToString(); // update UI text
            scoreIncTimer = 0f; // reset timer
        }

        // floor speed increasing until it is greater than max floor speed [FloorMovement.cs]
        if (vertObjSpeed < maxObjSpeed)
        {
            vertObjSpeed += objSpeedInc * Time.deltaTime;
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

        deathMenu.SetActive(true); // enable pause menu
        pauseButton.SetActive(false); // disable pause button
    }
}