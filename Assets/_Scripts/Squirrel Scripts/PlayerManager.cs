using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public AudioClip musicLoop, pauseLoop, explosionSound;
    public bool playerDead { get; set; }

    private GameObject gameOverMenu;
    private GameObject pauseMenu;
    private CameraController camControl;

    private AudioSource music;
    private AudioSource[] atmosphere;
    private float musicTime;

    void Awake () {
        pauseMenu = GameObject.Find("/Pause Menu");
        camControl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        gameOverMenu = GameObject.Find("/GameOver Menu");
        pauseMenu.GetComponent<Canvas>().enabled = false;

        music = GetComponent<AudioSource> ();
        atmosphere = GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<AudioSource>();
        music.clip = musicLoop;
        gameOverMenu.GetComponent<Canvas>().enabled = false;
        playerDead = false;
    }

    void Update() {
        if (Input.GetButtonDown("Cancel")) {
                SetPauseMenuState();
        }
    }

    public void SetPauseMenuState() {
        if (!gameOverMenu.GetComponent<Canvas>().enabled) {
            if (pauseMenu.GetComponent<Canvas>().enabled) {
                pauseMenu.GetComponent<Canvas>().enabled = false;
                pauseMenu.GetComponent<PauseMenu>().Deactivate();
                Time.timeScale = 1.0f;
                switchMusic();
                for (int a = 0; a < atmosphere.Length; a++) {
                    atmosphere[a].Play();
                }
            } else {
                pauseMenu.GetComponent<Canvas>().enabled = true;
                pauseMenu.GetComponent<PauseMenu>().Activate();
                Time.timeScale = 0;
                switchMusic();
                for (int i = 0; i < atmosphere.Length; i++) {
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

        playerDead = true;
        camControl.Shake();
        GetComponents<AudioSource>()[1].Play();
        gameOverMenu.GetComponent<Canvas>().enabled = true;
        gameOverMenu.GetComponent<GameOverMenu>().active = true;
        gameOverMenu.GetComponent<GameOverMenu>().PlayerDead();
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
