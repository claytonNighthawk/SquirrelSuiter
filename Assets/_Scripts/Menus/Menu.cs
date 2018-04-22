using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour {
    protected Canvas canvas;

    public virtual void SetActive(bool isActive) {
        canvas.enabled = isActive;
        StartMenu.SetActiveMenuButtons(!isActive);
    }

    void Start() {
        if (!PlayerPrefs.HasKey("cameraY")) {
            print("cameraY doesnt exist!");
            PlayerPrefs.SetFloat("cameraY", 17.0f);
            PlayerPrefs.SetFloat("cameraZ", -7.0f);
        }
    }
}