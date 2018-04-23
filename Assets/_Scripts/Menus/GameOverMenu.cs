using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class GameOverMenu : MonoBehaviour {
    private Button[] buttons;
    private Text newHighScore;
    private Score scoreScript;
    private PlayerManager pm;

    void Start() {
        GetComponent<Canvas>().enabled = false;
        buttons = GetComponentsInChildren<Button>();
        scoreScript = GameObject.Find("/ScoreCanvas").GetComponent<Score>();
        pm = GameObject.Find("/Player Manager").GetComponent<PlayerManager>();
    }

    public void PlayerDead() {
        GetComponent<Canvas>().enabled = true;
    #if !MOBILE_INPUT
        buttons[0].Select();
    #endif
        transform.Find("Acorns").GetComponent<Text>().text = "Acorns: " + scoreScript.acornNum.ToString();
        transform.Find("Score").GetComponent<Text>().text = "Score: " + scoreScript.scoreNum.ToString();

        if (scoreScript.prevHigh >= scoreScript.scoreNum) {
            transform.Find("New High Score").GetComponent<Text>().enabled = false;
        }
    }

    public void MainMenuPress() {
        SceneManager.LoadScene(0);
    }

    public void RestartPress() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Update() {
        if (EventSystem.current.currentSelectedGameObject == null && pm.playerDead) {
            buttons[2].Select();
        }
    }
}
