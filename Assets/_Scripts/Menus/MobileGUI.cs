using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MobileGUI : MonoBehaviour {

    public PlayerManager pm;
    public Canvas mobileGUI;
    public Button pauseButton;


	void Start () {
        pauseButton = pauseButton.GetComponent<Button>();
        mobileGUI = mobileGUI.GetComponent<Canvas>();
    #if !MOBILE_INPUT
        mobileGUI.enabled = false;
        pauseButton.enabled = false;
    #endif
    }
	
	public void PauseGame() {
        pm.ActivatePauseMenu();
    }
}
