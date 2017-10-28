using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class PauseMenu : MonoBehaviour {

    public Button mainButton;
    public Button restartButton;
    public EventSystem es;
    public Button placeholderPause;
    public bool active = false;

    void Start() {
        mainButton = mainButton.GetComponent<Button>();
        restartButton = restartButton.GetComponent<Button>();
        mainButton.enabled = true;
        restartButton.enabled = true;
    }

    void Update() {
        if (es.currentSelectedGameObject == null && active) {
            placeholderPause.Select();
        }
    }

    public void Activate() {
        active = true;
    #if !MOBILE_INPUT
        if (Input.GetJoystickNames().Length > 0 && Input.GetJoystickNames()[0] != "") {
            restartButton.Select();
        }
    #endif
    }

    public void Deactivate() {
        active = false;
        placeholderPause.Select();
    }

    public void ReturnPress() {
        SceneManager.LoadScene(0);
    }

    public void RestartPress() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}