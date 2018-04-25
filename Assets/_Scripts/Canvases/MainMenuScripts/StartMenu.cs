using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartMenu : Menu {
    static Button[] mainMenuButtons;

    void Awake() {
        canvas = GetComponent<Canvas>();
        mainMenuButtons = GetComponentsInChildren<Button>();
    }

    public static void SetActiveMenuButtons(bool isActive) {
        foreach (Button menuButton in mainMenuButtons) {
            menuButton.enabled = isActive;
        }
    }
}
