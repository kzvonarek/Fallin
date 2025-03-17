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
    public void closeGame()
    {
        Application.Quit();
    }
    public void encyclopediaButton()
    {
        Debug.Log("Encyclopedia button clicked");
    }
    public void shopButton()
    {
        Debug.Log("Shop button clicked");
    }
    public void infoButton()
    {
        Debug.Log("Info button clicked");
    }
    public void settingsButton()
    {
        Debug.Log("Settings button clicked");
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
}