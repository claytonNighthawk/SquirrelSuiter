using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    public override void SetActive(bool isActive) {
        if (isActive) {
            string menuName = EventSystem.current.currentSelectedGameObject.tag;
            print(menuName);
            Canvas menu = GameObject.Find(menuName).GetComponent<Canvas>();            
            //Canvas menu = menu1;
            print(menu);
            menu.enabled = true;
        } else {
            canvas.enabled = isActive;
        }
        SetActiveMenuButtons(!isActive);
    }
}
