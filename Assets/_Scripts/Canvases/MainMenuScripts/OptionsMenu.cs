using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionsMenu : Menu {

    void Awake() {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;

    #if !MOBILE_INPUT
        var mobileOptionsButton = transform.Find("MobileOptions").gameObject;
        mobileOptionsButton.SetActive(false);
    #endif
    }

    public override void ActivateMenu() {
        base.ActivateMenu();
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
