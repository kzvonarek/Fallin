using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // main menus
    public void playGame()
    {
        SceneManager.LoadScene("Main Scene");
    }

    /* for possible PC version
    public void closeGame()
    {
        Application.Quit();
    }
    */

    // during gameplay
    public void menuButton()
    {
        SceneManager.LoadScene("Main Menu");
    }
}