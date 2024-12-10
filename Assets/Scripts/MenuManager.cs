using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Main Menu Scene Buttons ---=---
    public void playGame()
    {
        SceneManager.LoadScene("Game Play");
    }

    // Game Play Scene Buttons ---=---
    public void menuButton()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void playAgain()
    {
        SceneManager.LoadScene("Game Play");
    }

    /* for possible PC version
    public void closeGame()
    {
        Application.Quit();
    }
    */
}