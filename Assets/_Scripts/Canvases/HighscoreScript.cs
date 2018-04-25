using UnityEngine;
using UnityEngine.UI;

public class HighscoreScript : MonoBehaviour {
    public Text highScore1;
    public Text highScore2;
    public Text highScore3;
    public Text highScore4;
    public Text highScore5;

    void Start () {
        SetHighscores();
    }

    public void ResetHighscore() {
        PlayerPrefs.SetInt("HighScore1", 0);
        PlayerPrefs.SetInt("HighScore2", 0);
        PlayerPrefs.SetInt("HighScore3", 0);
        PlayerPrefs.SetInt("HighScore4", 0);
        PlayerPrefs.SetInt("HighScore5", 0);
        PlayerPrefs.SetInt("HighScore6", 0);
        SetHighscores();
    }

    public void SetHighscores() {
        int prevHigh;
        prevHigh = PlayerPrefs.GetInt("HighScore1", 0);
        highScore1.text = prevHigh.ToString();
        prevHigh = PlayerPrefs.GetInt("HighScore2", 0);
        highScore2.text = prevHigh.ToString();
        prevHigh = PlayerPrefs.GetInt("HighScore3", 0);
        highScore3.text = prevHigh.ToString();
        prevHigh = PlayerPrefs.GetInt("HighScore4", 0);
        highScore4.text = prevHigh.ToString();
        prevHigh = PlayerPrefs.GetInt("HighScore5", 0);
        highScore5.text = prevHigh.ToString();
    }
}
