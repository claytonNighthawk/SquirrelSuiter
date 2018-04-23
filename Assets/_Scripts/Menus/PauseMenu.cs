using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class PauseMenu : MonoBehaviour {
    private PlayerManager pm;
    private Button[] buttons;

    void Start() {
        buttons = GetComponentsInChildren<Button>();
        GetComponent<Canvas>().enabled = false;
        pm = GameObject.Find("/Player Manager").GetComponent<PlayerManager>();
    }

    void Update() {
        if (EventSystem.current.currentSelectedGameObject == null && Time.timeScale == 0) {
            buttons[2].Select();
        }
    }

    public void SetMenuState() {
        if (GetComponent<Canvas>().enabled) {
            GetComponent<Canvas>().enabled = false;
            EventSystem.current.SetSelectedGameObject(null);
            Time.timeScale = 1.0f;
            pm.SwitchMusic();
            pm.SetAtmosphere(true);
        } else {
            GetComponent<Canvas>().enabled = true;
        #if !MOBILE_INPUT
            if (Input.GetJoystickNames().Length > 0 && Input.GetJoystickNames()[0] != "") {
                buttons[1].Select();
            }
        #endif
            Time.timeScale = 0.0f;
            pm.SwitchMusic();
            pm.SetAtmosphere(false);
        }
    }

    public void RestartOrMenuPress(bool restart) {
        Time.timeScale = 1;
        if (restart) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        } else {
            SceneManager.LoadScene("Menu");
        }
    }
}