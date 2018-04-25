using UnityEngine;

public class ExitMenu : Menu {

    void Awake() {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    public void ExitGame() {
        Application.Quit();
    }
}
