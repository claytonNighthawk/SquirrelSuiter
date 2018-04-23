using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    protected Canvas canvas;

    public virtual void SetActive(bool isActive) {
        canvas.enabled = isActive;
        StartMenu.SetActiveMenuButtons(!isActive);
    #if !MOBILE_INPUT
        GameObject blankButton = isActive ? GetComponentInChildren<Button>().gameObject : transform.parent.GetComponentInChildren<Button>().gameObject;
        EventSystem.current.SetSelectedGameObject(blankButton);
    #endif
    }

    public virtual void ActivateMenu() {
        string menuName = EventSystem.current.currentSelectedGameObject.tag;
        GameObject.Find(menuName).GetComponent<Menu>().SetActive(true);
    }

    void Start() {
        if (!PlayerPrefs.HasKey("cameraY")) {
            print("cameraY doesnt exist!");
            PlayerPrefs.SetFloat("cameraY", 17.0f);
            PlayerPrefs.SetFloat("cameraZ", -7.0f);
        }
    }
}