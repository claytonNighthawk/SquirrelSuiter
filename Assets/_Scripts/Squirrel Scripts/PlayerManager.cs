using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public AudioClip musicLoop, pauseLoop;
    public bool playerDead { get; set; }

    private GameOverMenu gameOverMenu;
    private PauseMenu pauseMenu;
    private CameraController camControl;

    private AudioSource music;
    private AudioSource[] atmosphere;
    private float musicTime;

    void Awake () {
        pauseMenu = GameObject.Find("/Pause Menu").GetComponent<PauseMenu>();
        camControl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        gameOverMenu = GameObject.Find("/GameOver Menu").GetComponent<GameOverMenu>();

        music = GetComponent<AudioSource> ();
        atmosphere = GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<AudioSource>();
        music.clip = musicLoop;
        playerDead = false;
    }

    void Update() {
        if (Input.GetButtonDown("Cancel") && !playerDead) {
            pauseMenu.SetMenuState();
        }
    }

    public void Die() {
        SwitchMusic();
        playerDead = true;
        camControl.Shake();

        GetComponents<AudioSource>()[1].Play();
        gameOverMenu.PlayerDead();
    }

    public void SetAtmosphere(bool playing) {
        if (playing) {
            for (int a = 0; a < atmosphere.Length; a++) {
                atmosphere[a].Play();
            }
        } else {
            for (int i = 0; i < atmosphere.Length; i++) {
                atmosphere[i].Stop();
            }
        }
    }

    public void SwitchMusic() {
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
