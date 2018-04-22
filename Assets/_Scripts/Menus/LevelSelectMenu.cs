using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelSelectMenu : Menu {
    //Button[] levelSelectButtons;

    void Awake() {
        canvas = GetComponent<Canvas>();
        //levelSelectButtons = GetComponentsInChildren<Button>();
        canvas.enabled = false;
    }

    public void SelectLevel() {
        string sceneName = EventSystem.current.currentSelectedGameObject.tag;
        SceneManager.LoadScene(sceneName);
    }
}
