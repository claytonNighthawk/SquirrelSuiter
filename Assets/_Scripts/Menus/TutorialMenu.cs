using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutorialMenu : Menu {
    Canvas canvas2;

    private string controllerTutorial1 = "Avoid obstacles. \n\nCollect acorns and stay alive to score points. \n\nUse the left stick to move. \n\nIf you would like inverted controls or a closer camera you can turn them on in the Options menu.";
    private string controllerTutorial2 = "To barrel roll, hold in the direction you would like to roll and press X. \n\nSome levels have boost pickups. Activate them by pressing B. \n\nStart will bring up a pause Menu.";
    private string mobileTutorial1 = "Avoid obstacles. \n\nCollect acorns and stay alive to score points. \n\nUse tilt controlls to move. \n\nYou can set inverted controls, the zero point for tilt controls, or a closer camera in the Options menu.";
    private string mobileTutorial2 = "Make short quick swipes left or right to barrel roll. \n\nSome levels have boost pickups, activate them by tapping the screen. \n\nTap in the lower right to bring up a pause menu.";

    void Awake() {
        Canvas[] tutorialCanvases = GetComponentsInChildren<Canvas>();
        canvas = tutorialCanvases[0];
        canvas2 = tutorialCanvases[1];

        canvas.enabled = false;
        canvas2.enabled = false;

    #if !MOBILE_INPUT
        if (Input.GetJoystickNames().Length > 0 && Input.GetJoystickNames()[0] != "") {
            GameObject.Find("Tutorial1/Body").GetComponent<Text>().text = controllerTutorial1;
            GameObject.Find("Tutorial2/Body").GetComponent<Text>().text = controllerTutorial2;
        }
    #else
        GameObject.Find("Tutorial1/Body").GetComponent<Text>().text = mobileTutorial1;
        GameObject.Find("Tutorial2/Body").GetComponent<Text>().text = mobileTutorial2;
    #endif
    }

    public void Next() {
        canvas.enabled = false;
        canvas2.enabled = true;
    #if !MOBILE_INPUT
        EventSystem.current.SetSelectedGameObject(canvas2.gameObject.GetComponentInChildren<Button>().gameObject);
    #endif
    }

    public override void SetActive(bool isActive) {
        canvas.enabled = isActive;
        StartMenu.SetActiveMenuButtons(!isActive);

        Button[] buttons;
        if (isActive) {
            buttons = transform.GetChild(0).GetComponentsInChildren<Button>();
        } else {
            canvas2.enabled = isActive;
            buttons = transform.parent.transform.GetChild(0).GetComponentsInChildren<Button>();
        }
        int activeIndex = Input.GetJoystickNames().Length > 0 && Input.GetJoystickNames()[0] != "" ? 0 : buttons.Length - 1;
        EventSystem.current.SetSelectedGameObject(buttons[activeIndex].gameObject);
    }
}
