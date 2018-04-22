using UnityEngine;

public class CreditsMenu : Menu {
    //public Button creditsExit;

    void Awake() {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }
}
