using UnityEngine;

public class MobileController : MonoBehaviour {

    public SquirrelController sqc;
    public PlayerManager pm;
    private float minSwipeDist = 5;
    private float swipeDirection;

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
                    swipeDirection = Mathf.Sign(touch.deltaPosition.x);
                    sqc.StartBarrelRoll((int)swipeDirection);
                } else {
                    sqc.StartSpeedBoost();
                }
            }
        }
    }
}
