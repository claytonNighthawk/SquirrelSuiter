using UnityEngine;

public class OptionsMenu : Menu {
    // public Button optionsSetInverted;
    // public Button optionsMobileMenu;

    void Awake() {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    public void SetControlType(int ctrlDirection) {
        PlayerPrefs.SetInt("ctrlDirection", ctrlDirection);
    }

    public void SetCameraStyle(bool isFollowStyle) {
        float cameraY;
        float cameraZ;
        if (isFollowStyle) {
            cameraY = 17.0f;
            cameraZ = -7.0f;
        } else {
            cameraY = 15.35f;
            cameraZ = 0.0f;
        }
        PlayerPrefs.SetFloat("cameraY", cameraY);
        PlayerPrefs.SetFloat("cameraZ", cameraZ);
    }


}
