using UnityEngine;

public class CreditsMenu : Menu {

    void Awake() {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }
}
