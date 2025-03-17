using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Main Menu UI Objects ---=---
    [SerializeField] GameObject encyclopediaUI;
    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject infoUI;
    [SerializeField] GameObject settingsUI;

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
        encyclopediaUI.SetActive(true);

        // close others if opened
        shopUI.SetActive(false);
        infoUI.SetActive(false);
        settingsUI.SetActive(false);
    }
    public void shopButton()
    {
        shopUI.SetActive(true);

        // close others if opened
        encyclopediaUI.SetActive(false);
        infoUI.SetActive(false);
        settingsUI.SetActive(false);
    }
    public void infoButton()
    {
        infoUI.SetActive(true);

        // close others if opened
        encyclopediaUI.SetActive(false);
        shopUI.SetActive(false);
        settingsUI.SetActive(false);
    }
    public void settingsButton()
    {
        settingsUI.SetActive(true);

        // close others if opened
        encyclopediaUI.SetActive(false);
        shopUI.SetActive(false);
        infoUI.SetActive(false);
    }
    public void closeMenu()
    {
        encyclopediaUI.SetActive(false);
        shopUI.SetActive(false);
        infoUI.SetActive(false);
        settingsUI.SetActive(false);
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