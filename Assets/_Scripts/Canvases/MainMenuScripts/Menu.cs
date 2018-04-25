using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    protected Canvas canvas;

    public virtual void SetActive(bool isActive) {
        canvas.enabled = isActive;
        StartMenu.SetActiveMenuButtons(!isActive);

    #if !MOBILE_INPUT
        Button[] buttons = isActive ? GetComponentsInChildren<Button>() : transform.parent.transform.GetChild(0).GetComponentsInChildren<Button>();
        int activeIndex = Input.GetJoystickNames().Length > 0 && Input.GetJoystickNames()[0] != "" ? 0 : buttons.Length - 1;
        EventSystem.current.SetSelectedGameObject(buttons[activeIndex].gameObject);
    #endif
    }

    public virtual void ActivateMenu() {
        string menuName = EventSystem.current.currentSelectedGameObject.tag;
        GameObject.Find(menuName).GetComponent<Menu>().SetActive(true);
    }
}