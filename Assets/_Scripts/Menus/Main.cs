using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public abstract class Menu : MonoBehaviour {
    protected Canvas canvas;

    public virtual void SetActive(bool isActive) {
        canvas.enabled = isActive;
    }
}

public class StartMenu : Menu {
    Button[] mainMenuButtons;

    public StartMenu() {
        canvas = GameObject.FindGameObjectWithTag("StartMenu").GetComponent<Canvas>();
        mainMenuButtons = canvas.GetComponentsInChildren<Button>();        
    }

    public void SetActiveMenuButtons(bool isActive) {
        foreach (Button menuButton in mainMenuButtons) {
            menuButton.enabled = isActive;
        }
    }

    public void SetActiveMenu(Menu menu, bool isMenuActive) {
        menu.SetActive(isMenuActive);
        SetActiveMenuButtons(!isMenuActive);
        //currentBlankButton = 4;
        //es.SetSelectedGameObject(null);

        //if (controllerName != "") {
        //    level1.Select();
        //}
    }
}

public class LevelSelect : Menu {
    Button[] levelSelectButtons;

    public LevelSelect() {
        canvas = GameObject.FindGameObjectWithTag("StartMenu").GetComponent<Canvas>();
        levelSelectButtons = canvas.GetComponentsInChildren<Button>();
        canvas.enabled = false;
    }

    public void SelectLevel() {
        string sceneName = EventSystem.current.currentSelectedGameObject.tag;
        SceneManager.LoadScene(sceneName);
    }
}

public class OptionsMenu : Menu {
    // public Button optionsSetInverted;
    // public Button optionsMobileMenu;

    public OptionsMenu() {
        canvas = GameObject.FindGameObjectWithTag("OptionsMenu").GetComponent<Canvas>();
        canvas.enabled = false;
    }

    public void SetControlType(int ctrlDirection) {
        PlayerPrefs.SetInt("ctrlDirection", ctrlDirection);
    }

    //public void SetInvertedControls() {
    //    PlayerPrefs.SetInt("ctrlDirection", -1);
    //}

    //public void SetNormalControlls() {
    //    PlayerPrefs.SetInt("ctrlDirection", 1);
    //}

    public void SetCameraDistance(float cameraY, float cameraZ) {
        PlayerPrefs.SetFloat("cameraY", cameraY);
        PlayerPrefs.SetFloat("cameraZ", cameraZ);
    }

    //public void SetThirdPersonCamera() {
    //    //print("setting 3rd person camera");
    //    PlayerPrefs.SetFloat("cameraY", 15.35f);
    //    PlayerPrefs.SetFloat("cameraZ", 0f);
    //}

    //public void SetFollowCamera() {
    //    //print("setting follow camera");
    //    PlayerPrefs.SetFloat("cameraY", 17.0f);
    //    PlayerPrefs.SetFloat("cameraZ", -7.0f);
    //}
}

public class MobileOptionsMenu : Menu {
    // public Text rotationText;

    public MobileOptionsMenu() {
        canvas = GameObject.FindGameObjectWithTag("MobileOptionsMenu").GetComponent<Canvas>();
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
        GameObject.Find("RotationText").GetComponent<Text>().text = rotateQuaternion.eulerAngles.ToString();
    }

    public void SetSwipeDist(int dist) {
        // dist should be either 5 or 10
        PlayerPrefs.SetFloat("swipeDist", dist);
    }
}

public class TutorialMenu : Menu {
    Canvas canvas2;

    //private string controllerTutorial1 = "Avoid obstacles. \n\nCollect acorns and stay alive to score points. \n\nUse the left stick to move. \n\nIf you would like inverted controls or a closer camera you can turn them on in the Options menu.";
    //private string controllerTutorial2 = "To barrel roll, hold in the direction you would like to roll and press X. \n\nSome levels have boost pickups. Activate them by pressing B. \n\nStart will bring up a pause Menu.";
    //private string mobileTutorial1 = "Avoid obstacles. \n\nCollect acorns and stay alive to score points. \n\nUse tilt controlls to move. \n\nYou can set inverted controls, the zero point for tilt controls, or a closer camera in the Options menu.";
    //private string mobileTutorial2 = "Make short quick swipes left or right to barrel roll. \n\nSome levels have boost pickups, activate them by tapping the screen. \n\nTap in the lower right to bring up a pause menu.";

    public TutorialMenu() {
        GameObject[] tutorialObjects = GameObject.FindGameObjectsWithTag("TutorialMenu");
        canvas = tutorialObjects[0].GetComponent<Canvas>();
        canvas2 = tutorialObjects[1].GetComponent<Canvas>();

        canvas.enabled = false;
        canvas2.enabled = false;
    }

    public void Next() {
        canvas.enabled = false;
        canvas2.enabled = true;
        //currentBlankButton = 3;
        //es.SetSelectedGameObject(null);

        //if (controllerName != "") {
        //    tutorial2Body.text = controllerTutorial2;
        //    tutorial2Exit.Select();
        //} else if (mobile) {
        //    tutorial2Body.text = mobileTutorial2;
        //}
    }

}

public class CreditsMenu : Menu {
    //public Button creditsExit;

    public CreditsMenu() {
        canvas = GameObject.FindGameObjectWithTag("CreditsMenu").GetComponent<Canvas>();
        canvas.enabled = false;
    }
}

public class ExitMenu : Menu {
    //public Button exitYes;
    //public Button exitNo;

    public ExitMenu() {
        canvas = GameObject.FindGameObjectWithTag("ExitMenu").GetComponent<Canvas>();
        canvas.enabled = false;
    }

    public void ExitGame() {
        Application.Quit();
    }
}

public class Main : Menu {

    public StartMenu startMenu;
    public LevelSelect levelSelectMenu;
    public OptionsMenu optionsMenu;
    public MobileOptionsMenu mobileOptionsMenu;
    public TutorialMenu tutorialMenu;
    public CreditsMenu creditsMenu;
    public ExitMenu exitMenu;

    // public Canvas levelSelect;
    // public Canvas optionsMenu;
    // public Canvas mobileOptions;
    // public Canvas tutorial1;
    // public Canvas tutorial2;
    // public Canvas credits;
    // public Canvas exitMenu;

    // public EventSystem es;
    // public Button[] placeholderButtons; // placeholder buttons exist so the game can switch back and forth from controller to keyboard controlls on the fly without the buttons breaking

    //public float smallSwipeDist = 5;
    //public float largeSwipeDist = 10;

    //private int currentBlankButton = 0;
    //private string controllerName = "";
    //private bool mobile = false;  

    //private string controllerTutorial1 = "Avoid obstacles. \n\nCollect acorns and stay alive to score points. \n\nUse the left stick to move. \n\nIf you would like inverted controls or a closer camera you can turn them on in the Options menu.";
    //private string controllerTutorial2 = "To barrel roll, hold in the direction you would like to roll and press X. \n\nSome levels have boost pickups. Activate them by pressing B. \n\nStart will bring up a pause Menu.";
    //private string mobileTutorial1 = "Avoid obstacles. \n\nCollect acorns and stay alive to score points. \n\nUse tilt controlls to move. \n\nYou can set inverted controls, the zero point for tilt controls, or a closer camera in the Options menu.";
    //private string mobileTutorial2 = "Make short quick swipes left or right to barrel roll. \n\nSome levels have boost pickups, activate them by tapping the screen. \n\nTap in the lower right to bring up a pause menu.";

    void Start() {
        startMenu = new StartMenu();
        levelSelectMenu = new LevelSelect();
        tutorialMenu = new TutorialMenu();
        creditsMenu = new CreditsMenu();
        exitMenu = new ExitMenu();

        if (!PlayerPrefs.HasKey("cameraY")) {
            print("cameraY doesnt exist!");
            PlayerPrefs.SetFloat("cameraY", 17.0f);
            PlayerPrefs.SetFloat("cameraZ", -7.0f);
        }

    //#if !MOBILE_INPUT
    //    mobileOptionsMenu.gameObject.SetActive(false);
    //    if (Input.GetJoystickNames().Length > 0) {
    //        controllerName = Input.GetJoystickNames()[0];
    //    }
    //#else
    //    mobile = true;
    //#endif

        //if (controllerName != "") {
        //    startMenu.mainMenuButtons[0].Select();
        //}
    }

    public void SelectLevel() {
        levelSelectMenu.SelectLevel();
    }

    public void SetActiveMenu(bool isMenuActive) {
        string selectedButton = EventSystem.current.currentSelectedGameObject.tag;
        Menu menu = GameObject.FindGameObjectWithTag(selectedButton).GetComponent<Menu>();
        menu.SetActive(isMenuActive);
        startMenu.SetActiveMenuButtons(!isMenuActive);
    }

    //void Update() {
    //    if (es.currentSelectedGameObject == null) {
    //        placeholderButtons[currentBlankButton].Select();
    //    }
    //}

    //public void PlayPress() {
    //    levelSelect.enabled = true;
    //    startMenu.DisableMainMenuButtons();
    //    currentBlankButton = 4;
    //    es.SetSelectedGameObject(null);

    //    if (controllerName != "") {
    //        level1.Select();
    //    }        
    //}

    //public void ExitLevelSelect() {
    //    levelSelect.enabled = false;
    //    startMenu.EnableableMainMenuButtons();
    //    currentBlankButton = 0;
    //    es.SetSelectedGameObject(null);

    //    if (controllerName != "") {
    //        startMenu.mainMenuButtons[0].Select();
    //    }
    //}

    //public void OptionsPress() {
    //    optionsMenu.enabled = true;
    //    mobileOptions.enabled = false;
    //    startMenu.DisableMainMenuButtons();
    //    currentBlankButton = 5;
    //    es.SetSelectedGameObject(null);

    //    if (controllerName != "") {
    //        optionsSetInverted.Select();
    //    }
    //}

    //public void SetInvertedControls() {
    //    PlayerPrefs.SetInt("ctrlDirection", -1);
    //}

    //public void SetNormalControlls() {
    //    PlayerPrefs.SetInt("ctrlDirection", 1);
    //}

    //public void SetThirdPersonCamera() {
    //    print("setting 3rd person camera");
    //    PlayerPrefs.SetFloat("cameraY", 15.35f);
    //    PlayerPrefs.SetFloat("cameraZ", 0f);
    //}

    //public void SetFollowCamera() {
    //    print("setting follow camera");
    //    PlayerPrefs.SetFloat("cameraY", 17.0f);
    //    PlayerPrefs.SetFloat("cameraZ", -7.0f);
    //}

    //public void OptionsMobileMenu() {
    //    optionsMenu.enabled = false;
    //    mobileOptions.enabled = true;
    //}

    //public void CalibrateRotation() {
    //    Vector3 accelerationSnapshot = Input.acceleration;
    //    Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
    //    Quaternion calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    //    PlayerPrefs.SetFloat("calibrationW", calibrationQuaternion.w);
    //    PlayerPrefs.SetFloat("calibrationX", calibrationQuaternion.x);
    //    PlayerPrefs.SetFloat("calibrationY", calibrationQuaternion.y);
    //    PlayerPrefs.SetFloat("calibrationZ", calibrationQuaternion.z);
    //    rotationText.text = rotateQuaternion.eulerAngles.ToString();
    //}

    //public void SetLargeSwipeDist() {
    //    PlayerPrefs.SetFloat("swipeDist", largeSwipeDist);
    //}

    //public void SetSmallSwipeDist() {
    //    PlayerPrefs.SetFloat("swipeDist", smallSwipeDist);
    //}

    //public void OptionsExit() {
    //    optionsMenu.enabled = false;
    //    mobileOptions.enabled = false;
    //    startMenu.EnableableMainMenuButtons();
    //    currentBlankButton = 0;
    //    es.SetSelectedGameObject(null);

    //    if (controllerName != "") {
    //        startMenu.mainMenuButtons[2].Select();
    //    }
    //}

    //public void CreditsPress() {
    //    credits.enabled = true;
    //    startMenu.DisableMainMenuButtons();
    //    currentBlankButton = 6;
    //    es.SetSelectedGameObject(null);
    //}

    //public void CreditsExit() {
    //    credits.enabled = false;
    //    startMenu.EnableableMainMenuButtons();
    //    currentBlankButton = 0;
    //    es.SetSelectedGameObject(null);
    //}

    //public void TutorialPress() {
    //    tutorial1.enabled = true;
    //    startMenu.DisableMainMenuButtons();
    //    currentBlankButton = 2;
    //    es.SetSelectedGameObject(null);

    //    if (controllerName != "") {
    //        tutorial1Body.text = controllerTutorial1;
    //        tutorial1Next.Select();
    //    } else if (mobile) {
    //        tutorial1Body.text = mobileTutorial1;
    //    }
    //}

    //public void TutorialNext() {
    //    tutorial1.enabled = false;
    //    tutorial2.enabled = true;
    //    currentBlankButton = 3;
    //    es.SetSelectedGameObject(null);

    //    if (controllerName != "") {
    //        tutorial2Body.text = controllerTutorial2;
    //        tutorial2Exit.Select();
    //    } else if (mobile) {
    //        tutorial2Body.text = mobileTutorial2;
    //    }
    //}

    //public void TutorialExit() {
    //    tutorial1.enabled = false;
    //    tutorial2.enabled = false;
    //    startMenu.EnableableMainMenuButtons();
    //    currentBlankButton = 0;
    //    es.SetSelectedGameObject(null);

    //    if (controllerName != "") {
    //        startMenu.mainMenuButtons[3].Select();
    //    }
    //}

    //public void ExitPress() {
    //    exitMenu.enabled = true;
    //    startMenu.DisableMainMenuButtons();
    //    currentBlankButton = 1;
    //    es.SetSelectedGameObject(null);

    //    if (controllerName != "") {
    //        exitNo.Select();
    //    }
    //}

    //public void ExitNoPress() {
    //    exitMenu.enabled = false;
    //    startMenu.EnableableMainMenuButtons();
    //    currentBlankButton = 0;
    //    es.SetSelectedGameObject(null);

    //    if (controllerName != "") {
    //        startMenu.mainMenuButtons[4].Select();
    //    }
    //}

    //public void ExitYesPress() {
    //    Application.Quit();
    //}

}