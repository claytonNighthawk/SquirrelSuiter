using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class GameOverMenu : MonoBehaviour {
    public Button mainButton;
    public ScoreText scoreScript;
    public Button restartButton;
    public EventSystem es;
    public Button placeholderGameOver;
    public Text acornNum;
    public Text scoreNum;
    public Text newHighScore;
    
    public bool active = false;

    void Start() {
        mainButton = mainButton.GetComponent<Button>();
        scoreScript = scoreScript.GetComponent<ScoreText>();
    }

    public void PlayerDead() {
        scoreScript.playerDead = true;
    #if !MOBILE_INPUT
        restartButton.Select();
    #endif
        acornNum.text = "Acorns: " + scoreScript.acornNum.ToString();
        scoreNum.text = "Score: " + scoreScript.scoreNum.ToString();

        if (scoreScript.prevHigh >= scoreScript.scoreNum) {
            newHighScore.enabled = false;
        }
    }

    public void MainMenuPress() {
        SceneManager.LoadScene(0);
    }

    public void RestartPress() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Update() {
        if (es.currentSelectedGameObject == null && active) {
            placeholderGameOver.Select();
        }
    }
}
