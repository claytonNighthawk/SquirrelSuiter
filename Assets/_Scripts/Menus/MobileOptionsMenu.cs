using UnityEngine;
using UnityEngine.UI;

public class MobileOptionsMenu : Menu {
    // public Text rotationText;

    void Awake() {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    public void CalibrateRotation() {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
        Quaternion calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
        PlayerPrefs.SetFloat("calibrationW", calibrationQuaternion.w);
        PlayerPrefs.SetFloat("calibrationX", calibrationQuaternion.x);
        PlayerPrefs.SetFloat("calibrationY", calibrationQuaternion.y);
        PlayerPrefs.SetFloat("calibrationZ", calibrationQuaternion.z);
        GetComponent<Text>().text = rotateQuaternion.eulerAngles.ToString();
    }

    public void SetSwipeDist(int dist) {
        PlayerPrefs.SetFloat("swipeDist", dist);
    }
}
