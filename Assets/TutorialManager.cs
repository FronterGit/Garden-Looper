using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    [SerializeField] private Toggle tutorialToggle;
    [SerializeField] private bool tutorialEnabled = true;
    
    public void ToggleTutorial()
    {
        if (tutorialToggle == null) tutorialToggle = GameObject.FindWithTag("TutorialToggle").GetComponent<Toggle>();
        tutorialEnabled = tutorialToggle.isOn;
    }
    
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        MainMenuManager.toggleTutorialEvent += ToggleTutorial;
    }
    
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        MainMenuManager.toggleTutorialEvent -= ToggleTutorial;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            if (tutorialEnabled)
            {
                Time.timeScale = 0;
                GameObject.FindWithTag("Tutorial").SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                GameObject.FindWithTag("Tutorial").SetActive(false);
            }
        }
        if (scene.name == "MainMenu")
        {
            Time.timeScale = 1;
            tutorialToggle = GameObject.FindWithTag("TutorialToggle").GetComponent<Toggle>();
        }
    }
    
    public void CloseTutorial()
    {
        Time.timeScale = 1;
        GameObject.FindWithTag("Tutorial").SetActive(false);
    }
}
