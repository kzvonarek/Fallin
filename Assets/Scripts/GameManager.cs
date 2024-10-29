using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // floor speed for FloorMovement.cs
    public float vertObjSpeed;
    [SerializeField] float objSpeedInc;
    [SerializeField] float maxObjSpeed;

    // score functionality
    [SerializeField] TextMeshProUGUI inGameScoreText;
    private int score;
    private float scoreIncTimer = 0f;

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
            scoreIncTimer = 0f; // reset timer
        }

        // floor speed increasing until it is greater than max floor speed [FloorMovement.cs]
        if (vertObjSpeed < maxObjSpeed)
        {
            vertObjSpeed += objSpeedInc * Time.deltaTime;
        }
    }
}
