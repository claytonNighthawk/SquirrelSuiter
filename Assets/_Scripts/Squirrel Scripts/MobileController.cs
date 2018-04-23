using UnityEngine;

public class MobileController : MonoBehaviour {

    private float minSwipeDist = 5;

    public void Start() {
        if (PlayerPrefs.HasKey("swipeDist")) {
            minSwipeDist = PlayerPrefs.GetFloat("swipeDist");
        } else {
            minSwipeDist = 5;
        }
    }

    void FixedUpdate() {
        if (Input.touchCount > 0) {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Ended) {
                if (Mathf.Abs(touch.deltaPosition.sqrMagnitude) >= minSwipeDist * minSwipeDist) {
                    float swipeDirection = Mathf.Sign(touch.deltaPosition.x);
                    GetComponent<SquirrelController>().StartBarrelRoll((int)swipeDirection);
                } else {
                    GetComponent<SquirrelController>().StartSpeedBoost();
                }
            }
        }
    }
}
