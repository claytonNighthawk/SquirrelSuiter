using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {
    public Text score;
    public Text acorns;
    public Text highScore;
    public Text boosts;

    public int acornNum;
    public int scoreNum { get; set; }
    public int boostsAvailable { get; set; }
    public int prevHigh { get; set; }
    public int scoreThreshold = 2000;

    private PlayerManager pm;
    private string highScoreOnLevel;
    
    void Start () {
        scoreNum = acornNum = 0;
        acorns.text = "Acorns: 0";
        highScoreOnLevel = "HighScore" + SceneManager.GetActiveScene().buildIndex;
        prevHigh = PlayerPrefs.GetInt(highScoreOnLevel, 0);
        highScore.text = "High Score: \n" + prevHigh.ToString();
        pm = GameObject.Find("Player Manager").GetComponent<PlayerManager>();
    }

    void Update() {   
        if (Time.timeScale == 0 || pm.playerDead) {
            return;
        }

        scoreNum++;
        acorns.text = "Acorns: " + acornNum.ToString();
        score.text = "Score: " + scoreNum.ToString();
        boosts.text = "Boosts: " + boostsAvailable.ToString();
        
        if (scoreNum > prevHigh) {
            PlayerPrefs.SetInt(highScoreOnLevel, scoreNum);
            highScore.text = "High Score: \n" + scoreNum.ToString();
        }
    }

    public void IncAcorns() {
        acornNum++;
        scoreNum += 1000;
    }

}
