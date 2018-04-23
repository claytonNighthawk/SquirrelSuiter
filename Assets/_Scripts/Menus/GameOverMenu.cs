using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class GameOverMenu : MonoBehaviour {
    public bool active = false;

    private Button[] buttons;
    private Button restartButton;
    private Button placeholderGameOver;
    private Text acornNum;
    private Text scoreNum;
    private Text newHighScore;
    private Score scoreScript;

    void Start() {
        buttons = GetComponentsInChildren<Button>();
        restartButton = buttons[0];
        placeholderGameOver = buttons[2];
        scoreNum = transform.Find("Score").GetComponent<Text>();
        acornNum = transform.Find("Acorns").GetComponent<Text>();
        newHighScore = transform.Find("New High Score").GetComponent<Text>();
        scoreScript = GameObject.Find("ScoreCanvas").GetComponent<Score>();
    }

    public void PlayerDead() {
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
        if (EventSystem.current.currentSelectedGameObject == null && active) {
            placeholderGameOver.Select();
        }
    }
}
