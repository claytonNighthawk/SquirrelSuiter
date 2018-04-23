﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class PauseMenu : MonoBehaviour {

    private Button[] buttons;
    private bool active = false;

    void Start() {
        buttons = GetComponentsInChildren<Button>();
        buttons[0].enabled = true;
        buttons[1].enabled = true;
    }

    void Update() {
        if (EventSystem.current.currentSelectedGameObject == null && active) {
            buttons[2].Select();
        }
    }

    public void Activate() {
        active = true;
    #if !MOBILE_INPUT
        if (Input.GetJoystickNames().Length > 0 && Input.GetJoystickNames()[0] != "") {
            buttons[1].Select();
        }
    #endif
    }

    public void Deactivate() {
        active = false;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void RestartOrMenuPress(bool restart) {
        if (restart) {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        } else {
            Time.timeScale = 1;
            SceneManager.LoadScene("Menu");
        }
    }
}