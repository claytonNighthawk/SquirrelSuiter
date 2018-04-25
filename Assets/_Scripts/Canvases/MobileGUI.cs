using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MobileGUI : MonoBehaviour {
    private PauseMenu pauseMenu;

	void Start () {
        Canvas mobileGUI = GameObject.Find("/Mobile GUI").GetComponent<Canvas>();
        pauseMenu = GameObject.Find("/Pause Menu").GetComponent<PauseMenu>();
    #if !MOBILE_INPUT
        Button pauseButton = GetComponentInChildren<Button>();
        mobileGUI.enabled = false;
        pauseButton.enabled = false;
    #endif
    }

    public void PauseGame() {
        pauseMenu.SetMenuState();
    }
}
