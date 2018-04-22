using UnityEngine;

public class ExitMenu : Menu {
    //public Button exitYes;
    //public Button exitNo;

    void Awake() {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    public void ExitGame() {
        Application.Quit();
    }
}
