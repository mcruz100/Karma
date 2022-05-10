using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml;
using TMPro;
using UnityEngine.UI;

// Author: Matthew Cruz
// Purpose: Manage and control Screens

public class MenuManager : MonoBehaviour
{
    //Menus and Screens
    private GameObject MainMenuCanvas;
    private GameObject SettingsCanvas;
    private GameObject ControlsSettingsMenu;
    private GameObject VideoSettingsMenu;
    private GameObject AudioSettingsMenu;
    private GameObject EscCanvas;
    private GameObject VideoCanvas;
    private GameObject DemoCompleteCanvas;
    private GameObject MissionFailedCanvas;
    private RawImage Video; 
    private bool introPlayed = false;
    private bool startingScreens = false;

    [SerializeField] private AudioManager am;

    [SerializeField] private List<GameObject> MenuList = new List<GameObject>();
    //XML for Missions
    /*public TextMeshProUGUI currentMissionText;
    public TextMeshProUGUI missionText;
    public Transform missionContainer;
    public XmlDocument missionDataXml;*/
    
    public void Awake()
    {
        //Components of Menus
        ControlsSettingsMenu = GameObject.Find("ControlScrollView");
        VideoSettingsMenu = GameObject.Find("VideoScrollView");
        AudioSettingsMenu = GameObject.Find("AudioScrollView");
        VideoCanvas = GameObject.Find("VideoCanvas");
        DemoCompleteCanvas = GameObject.Find("DemoCompleteCanvas");
        MissionFailedCanvas = GameObject.Find("MissionFailedScreen");
        MainMenuCanvas = GameObject.Find("MainMenuCanvas");
        SettingsCanvas = GameObject.Find("SettingsCanvas");
        EscCanvas = GameObject.Find("EscCanvas");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        /*TextAsset xmlTextAsset =  (TextAsset) Resources.Load("Missions");
        missionDataXml = new XmlDocument();
        missionDataXml.LoadXml(xmlTextAsset.text);*/
    }

    private void Start()
    {
        Time.timeScale = 1;
        Scene currScene = SceneManager.GetActiveScene();
        if (currScene.name != "MainMenu" && startingScreens == false)
        {
            Debug.Log("Called Once");
            startingScreens = true;
            MainMenuCanvas.SetActive(false);
            EscCanvas.SetActive(false);
            SettingsCanvas.SetActive(false);
            MissionFailedCanvas.SetActive(false);
            DemoCompleteCanvas.SetActive(false);
        }
    }

    private void Update()
    {
        Scene currScene = SceneManager.GetActiveScene();
        if (currScene.name == "MainMenu" && introPlayed == false)
        {
            introPlayed = true;
            Invoke("DisableIntro", 25f);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //Skill Tree
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            //Escape Menu
            EscMenu();
        }
    }
    /*public void ShowCurrentMissions(XmlNode curMissionNode)
    {
        //This is bad code
        XmlNodeList missions = missionDataXml.SelectNodes("/GameEvents/Mission");

        for (int i = 0; i < missions.Count; i++)
        {
            if (i == 0)
            {
                missionText.SetText(missions[i].ChildNodes[0].InnerText);
            }
            else
            {
                currentMissionText.SetText(missions[i].ChildNodes[0].InnerText);
            }
        }
    }*/

    public void ReturnToGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        /*MissionFailedCanvas.SetActive(false);*/
        MainMenuCanvas.SetActive(false);
        EscCanvas.SetActive(false);
        SettingsCanvas.SetActive(false);
        DemoCompleteCanvas.SetActive(false);
    }

    /*
     * Loading Scene Functions
     * 
     */
    public void EnterTestRoomJKScene()
    {
        SceneManager.LoadScene("TestRoomJK");
    }
    public void EnterHospitalBasementScene()
    {
        SceneManager.LoadScene("HospitalBasement");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void DemoCompleteScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("Loading Demo Canvas");
        MainMenuCanvas.SetActive(false);
        EscCanvas.SetActive(false);
        SettingsCanvas.SetActive(false);
        /*MissionFailedCanvas.SetActive(false);*/
        DemoCompleteCanvas.SetActive(true);
    }
    public void EnterMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void EnterCityScene()
    {
        SceneManager.LoadScene("City");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1; // UnFreeze Game
    }

    public void EnterTestRoomAIScene()
    {
        SceneManager.LoadScene("TestAI");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void MissionFailedScreen()
    {
        Debug.Log("MissionFailed");
        MissionFailedCanvas.SetActive(true);
        /*MainMenuCanvas.SetActive(false);
        EscCanvas.SetActive(false);
        SettingsCanvas.SetActive(false);
        DemoCompleteCanvas.SetActive(false);
        Time.timeScale = 0;*/
    }

    public void RestartLevel()
    {
        MainMenuCanvas.SetActive(false);
        EscCanvas.SetActive(false);
        SettingsCanvas.SetActive(false);
        MissionFailedCanvas.SetActive(false);
        DemoCompleteCanvas.SetActive(false);
        SceneManager.LoadScene("HospitalBasement");
    }

    /*
     * Button Navigation for Menus
     * 
     */
    public void SettingsButton()
    {
        MainMenuCanvas.SetActive(false);
        EscCanvas.SetActive(false);
        SettingsCanvas.SetActive(true);
        ControlsSettingsMenu.SetActive(true);
        VideoSettingsMenu.SetActive(false);
        AudioSettingsMenu.SetActive(false);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public void EscMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        MainMenuCanvas.SetActive(false);
        EscCanvas.SetActive(true);
        SettingsCanvas.SetActive(false);
        /*MissionFailedCanvas.SetActive(false);*/
        DemoCompleteCanvas.SetActive(false);
        /*ShowCurrentMissions(missionDataXml);*/
    }

    //Buttons for the Settings Screen
    public void ControlsMenu()
    {
        ControlsSettingsMenu.SetActive(true);
        VideoSettingsMenu.SetActive(false);
        AudioSettingsMenu.SetActive(false);
    }
    public void VideoMenu()
    {
        ControlsSettingsMenu.SetActive(false);
        VideoSettingsMenu.SetActive(true);
        AudioSettingsMenu.SetActive(false);
    }
    public void AudioMenu()
    {
        ControlsSettingsMenu.SetActive(false);
        VideoSettingsMenu.SetActive(false);
        AudioSettingsMenu.SetActive(true);
    }

    public void ReturnToMainMenuNav()
    {
        MainMenuCanvas.SetActive(true);
        SettingsCanvas.SetActive(false);
        EscCanvas.SetActive(false);
    }
    public void DisableIntro()
    {
        Destroy(VideoCanvas);
        Debug.Log(MainMenuCanvas);
        MainMenuCanvas.SetActive(true);
        SettingsCanvas.SetActive(false);
        EscCanvas.SetActive(false);
    }
}