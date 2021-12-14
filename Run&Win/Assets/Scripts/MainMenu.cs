using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Text highScoreText;
    public Text coins;
    public int coin;
    void Start()
    {
        highScoreDisplay(); // work fine
        displayCoins(); // work fine
    }

    public void displayCoins()
    {
        coin = PlayerPrefs.GetInt("Coins");
        coins.text = coin.ToString();
        PlayerController.coinCount = 0;// to reset the coin after deth
    }

    public void highScoreDisplay()
    {
        highScoreText.text = ((int)PlayerPrefs.GetFloat("Highscore")).ToString();
    }

    public void playGame()
    {
        SceneManager.LoadScene("Scene No.1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
