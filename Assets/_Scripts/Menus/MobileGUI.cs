using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MobileGUI : MonoBehaviour {

    private PlayerManager pm;

	void Start () {
        Canvas mobileGUI = GameObject.Find("/Mobile GUI").GetComponent<Canvas>();
        pm = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
    #if !MOBILE_INPUT
        Button pauseButton = GetComponentInChildren<Button>();
        mobileGUI.enabled = false;
        pauseButton.enabled = false;
    #endif
    }

    public void PauseGame() {
        pm.SetPauseMenuState();
    }
}
