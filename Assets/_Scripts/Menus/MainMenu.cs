using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour {

    public Canvas exitMenu;
    public Canvas levelSelect;
    public Canvas optionsMenu;
    public Canvas mobileOptions;
    public Canvas tutorial1;
    public Canvas tutorial2;

    public Button[] mainMenuButtons;
    public Button level1;
    public Button level2;
    public Button level3;
    public Button level4;
    public Button level5;
    public Button levelSelectExit;

    public Button optionsSetInverted;
    public Button optionsMobileMenu;
    public Text optionsExit;
    public Text rotationText;

    public Text tutorial1Body;
    public Text tutorial2Body;
    public Button tutorial1Exit;
    public Button tutorial2Exit;
    public Button tutorial1Next;

    public Button exitYes;
    public Button exitNo;

    public EventSystem es;
    public Button[] placeholderButtons;

    public float smallSwipeDist = 5;
    public float largeSwipeDist = 10;

    private int currentBlankButton = 0;
    private string controllerName = "";
    private bool mobile = false;
    private Quaternion calibrationQuaternion;    

    private string controllerTutorial1 = "Avoid obstacles. \n\nCollect acorns and stay alive to score points. \n\nUse the left stick to move. \n\nIf you would like inverted controls or a closer camera you can turn them on in the Options menu.";
    private string controllerTutorial2 = "To barrel roll, hold in the direction you would like to roll and press X. \n\nSome levels have boost pickups. Activate them by pressing B. \n\nStart will bring up a pause Menu.";
    private string mobileTutorial1 = "Avoid obstacles. \n\nCollect acorns and stay alive to score points. \n\nUse tilt controlls to move. \n\nYou can set inverted controls, the zero point for tilt controls, or a closer camera in the Options menu.";
    private string mobileTutorial2 = "Make short quick swipes left or right to barrel roll. \n\nSome levels have boost pickups, activate them by tapping the screen. \n\nTap in the lower right to bring up a pause menu.";

    void Start() {
        tutorial1.enabled = false;
        tutorial2.enabled = false;
        exitMenu.enabled = false;
        levelSelect.enabled = false;
        optionsMenu.enabled = false;
        mobileOptions.enabled = false;

    #if !MOBILE_INPUT
        optionsMobileMenu.transform.gameObject.SetActive(false);
        //optionsExit.rectTransform.position.Set(0, -275, 0);
        //print(optionsExit.rectTransform.position);
        if (Input.GetJoystickNames().Length > 0) {
            controllerName = Input.GetJoystickNames()[0];
        }
    #else
        mobile = true;
    #endif

        if (controllerName != "") {
            mainMenuButtons[0].Select();
        }
    }

    void DisableMainMenuButtons() {
        foreach (Button menuButton in mainMenuButtons) {
            menuButton.enabled = false;
        }
    }

    void EnableableMainMenuButtons() {
        foreach (Button menuButton in mainMenuButtons) {
            menuButton.enabled = true;
        }
    }

    void Update() {
        if (es.currentSelectedGameObject == null) {
            placeholderButtons[currentBlankButton].Select();
        }
    }

    public void PlayPress() {
        levelSelect.enabled = true;
        DisableMainMenuButtons();
        currentBlankButton = 4;
        es.SetSelectedGameObject(null);

        if (controllerName != "") {
            level1.Select();
        }        
    }

    public void Level1Press() {
        SceneManager.LoadScene(1);
    }
    public void Level2Press() {
        SceneManager.LoadScene(2);
    }
    public void Level3Press() {
        SceneManager.LoadScene(3);
    }
    public void Level4Press() {
        SceneManager.LoadScene(4);
    }
    public void Level5Press() {
        SceneManager.LoadScene(5);
    }

    public void ExitLevelSelect() {
        levelSelect.enabled = false;
        EnableableMainMenuButtons();
        currentBlankButton = 0;
        es.SetSelectedGameObject(null);

        if (controllerName != "") {
            mainMenuButtons[0].Select();
        }
    }

    public void OptionsPress() {
        optionsMenu.enabled = true;
        mobileOptions.enabled = false;
        DisableMainMenuButtons();
        currentBlankButton = 5;
        es.SetSelectedGameObject(null);

        if (controllerName != "") {
            optionsSetInverted.Select();
        }
    }

    public void SetInvertedControls() {
        PlayerPrefs.SetInt("ctrlDirection", -1);
    }

    public void SetNormalControlls() {
        PlayerPrefs.SetInt("ctrlDirection", 1);
    }

    public void SetThirdPersonCamera() {
        print("setting 3rd person camera");
        PlayerPrefs.SetFloat("cameraY", 15.35f);
        PlayerPrefs.SetFloat("cameraZ", 0f);
    }

    public void SetFollowCamera() {
        print("setting follow camera");
        PlayerPrefs.SetFloat("cameraY", 17.0f);
        PlayerPrefs.SetFloat("cameraZ", -7.0f);
    }

    public void OptionsMobileMenu() {
        optionsMenu.enabled = false;
        mobileOptions.enabled = true;
    }

    public void CalibrateRotation() {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
        PlayerPrefs.SetFloat("calibrationW", calibrationQuaternion.w);
        PlayerPrefs.SetFloat("calibrationX", calibrationQuaternion.x);
        PlayerPrefs.SetFloat("calibrationY", calibrationQuaternion.y);
        PlayerPrefs.SetFloat("calibrationZ", calibrationQuaternion.z);
        rotationText.text = rotateQuaternion.eulerAngles.ToString();
    }

    public void SetLargeSwipeDist() {
        PlayerPrefs.SetFloat("swipeDist", largeSwipeDist);
    }

    public void SetSmallSwipeDist() {
        PlayerPrefs.SetFloat("swipeDist", smallSwipeDist);
    }

    public void OptionsExit() {
        optionsMenu.enabled = false;
        mobileOptions.enabled = false;
        EnableableMainMenuButtons();
        currentBlankButton = 0;
        es.SetSelectedGameObject(null);

        if (controllerName != "") {
            mainMenuButtons[2].Select();
        }
    }

    public void PracticePress() {
        SceneManager.LoadScene(6);
    }

    public void TutorialPress() {
        tutorial1.enabled = true;
        DisableMainMenuButtons();
        currentBlankButton = 2;
        es.SetSelectedGameObject(null);

        if (controllerName != "") {
            tutorial1Body.text = controllerTutorial1;
            tutorial1Next.Select();
        } else if (mobile) {
            tutorial1Body.text = mobileTutorial1;
        }
    }

    public void TutorialNext() {
        tutorial1.enabled = false;
        tutorial2.enabled = true;
        currentBlankButton = 3;
        es.SetSelectedGameObject(null);

        if (controllerName != "") {
            tutorial2Body.text = controllerTutorial2;
            tutorial2Exit.Select();
        } else if (mobile) {
            tutorial2Body.text = mobileTutorial2;
        }
    }

    public void TutorialExit() {
        tutorial1.enabled = false;
        tutorial2.enabled = false;
        EnableableMainMenuButtons();
        currentBlankButton = 0;
        es.SetSelectedGameObject(null);

        if (controllerName != "") {
            mainMenuButtons[3].Select();
        }
    }

    public void ExitPress() {
        exitMenu.enabled = true;
        DisableMainMenuButtons();
        currentBlankButton = 1;
        es.SetSelectedGameObject(null);

        if (controllerName != "") {
            exitNo.Select();
        }
    }

    public void ExitNoPress() {
        exitMenu.enabled = false;
        EnableableMainMenuButtons();
        currentBlankButton = 0;
        es.SetSelectedGameObject(null);

        if (controllerName != "") {
            mainMenuButtons[4].Select();
        }
    }

    public void ExitYesPress() {
        Application.Quit();
    }

}