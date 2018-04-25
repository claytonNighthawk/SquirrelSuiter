using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelSelectMenu : Menu {

    void Awake() {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    public void SelectLevel() {
        string sceneName = EventSystem.current.currentSelectedGameObject.tag;
        SceneManager.LoadScene(sceneName);
    }
}
