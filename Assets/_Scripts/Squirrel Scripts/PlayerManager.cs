using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {
    public Canvas pauseMenu;
    public PauseMenu pauseMenuScript;
    public CameraController camControl;
	public AudioClip musicLoop, pauseLoop;
    public Canvas gameOverCanvas;
    public GameOverMenu gameOverScript;
    public ScoreText scoreScript;

    private AudioSource music; 
	private AudioSource[] atmosphere;
	private float musicTime;

	void Start () {
        pauseMenu.enabled = false;
		music = GetComponent<AudioSource> ();
		atmosphere = GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<AudioSource>();
		music.clip = musicLoop;
        gameOverCanvas.enabled = false;
    }

    void Update() {
        if (Input.GetButtonDown("Cancel")) {
                ActivatePauseMenu();
		}
    }

    public void ActivatePauseMenu() {
        if (!gameOverCanvas.enabled) {
            if (pauseMenu.enabled) {
                pauseMenu.enabled = false;
                pauseMenuScript.Deactivate();
                Time.timeScale = 1.0f;
                switchMusic();
                int arraySize = atmosphere.Length;
                for (int a = 0; a < arraySize; a++) {
                    atmosphere[a].Play();
                }
            } else {
                pauseMenu.enabled = true;
                pauseMenuScript.Activate();
                Time.timeScale = 0;
                switchMusic();
                int arraySize = atmosphere.Length;
                for (int i = 0; i < arraySize; i++) {
                    atmosphere[i].Stop();
                }
            }
        }
    }

    public void Die() {
		musicTime = music.time;
		music.Stop();
		music.clip = pauseLoop;
		music.time = musicTime;
		music.Play();

        camControl.Shake();
        gameOverCanvas.enabled = true;
        gameOverScript.active = true;
        gameOverScript.PlayerDead();
    }

	public void switchMusic() {
		if (music.clip == pauseLoop) {
			musicTime = music.time;
            music.Stop();
			music.clip = musicLoop;
			music.time = musicTime;
			music.Play();
		} else {
			musicTime = music.time;
			music.Stop();
			music.clip = pauseLoop;
			music.time = musicTime;
			music.Play();
		}
	}
}
