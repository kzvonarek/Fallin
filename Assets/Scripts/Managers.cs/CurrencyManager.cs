using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    private GameObject gameManagerObj;
    private GameManager gMscript;

    void Start()
    {
        // allow updating coin count
        gameManagerObj = GameObject.FindWithTag("Game Manager");
        gMscript = gameManagerObj.GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Shield Effect"))
        {
            gMscript.totalCoins += 1;
            PlayerPrefs.SetInt("Total Coins", gMscript.totalCoins);
            PlayerPrefs.Save();

            gMscript.collectedCoins += 1;
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Destroy Zone"))
        {
            Destroy(this.gameObject);
        }
    }
}
