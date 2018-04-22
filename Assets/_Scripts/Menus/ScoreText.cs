using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreText : MonoBehaviour {
    public Text score;
    public Text acorns;
    public Text highScore;
    public Text boosts;

    [HideInInspector]
    public int scoreNum, acornNum, boostNum;
    [HideInInspector]
    public int prevHigh;
    [HideInInspector]
    public bool playerDead;

    private AcornScript AS;
    private string highScoreOnLevel;
    
    void Start () {
        playerDead = false;
        scoreNum = acornNum = 0;
        acorns.text = "Acorns: 0";
        highScoreOnLevel = "HighScore" + SceneManager.GetActiveScene().buildIndex;
        prevHigh = PlayerPrefs.GetInt(highScoreOnLevel, 0);
        highScore.text = "High Score: \n" + prevHigh.ToString();  
    }

    void Update() {   
        if (Time.timeScale == 0 || playerDead) {
            return;
        }

        scoreNum++;
        acorns.text = "Acorns: " + acornNum.ToString();
        score.text = "Score: " + scoreNum.ToString();
        boosts.text = "Boosts: " + boostNum.ToString ();
        
        if (scoreNum > prevHigh) {
            PlayerPrefs.SetInt(highScoreOnLevel, scoreNum);
            highScore.text = "High Score: \n" + scoreNum.ToString();
        }
    }

    public void IncAcorns() {
        acornNum++;
        scoreNum += 1000;
    }

    public void IncBoosts(){
        boostNum++;
    }

    public void DecBoosts(){
        boostNum--;
    }

}
