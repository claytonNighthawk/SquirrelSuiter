using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileOptionsMenu : OptionsMenu {
    private Text rotataionText;

    void Awake() {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        rotataionText = transform.Find("RotationText").GetComponent<Text>();
        rotataionText.text = "";
    }

    public void CalibrateRotation() {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
        Quaternion calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
        PlayerPrefs.SetFloat("calibrationW", calibrationQuaternion.w);
        PlayerPrefs.SetFloat("calibrationX", calibrationQuaternion.x);
        PlayerPrefs.SetFloat("calibrationY", calibrationQuaternion.y);
        PlayerPrefs.SetFloat("calibrationZ", calibrationQuaternion.z);
        rotataionText.text = rotateQuaternion.eulerAngles.ToString();
    }

    public void SetSwipeDist(int dist) {
        PlayerPrefs.SetFloat("swipeDist", dist);
    }
}
