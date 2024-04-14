using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Button switchLoopButton;
    
    public Color switchLoopOnColor;
    public Color switchLoopOffColor;
    
    public Image[] health = new Image[3];
    public GameObject sunPivot;
    private float timeToTurn;
    private bool sunStoneTurned;
    [SerializeField] private TMPro.TMP_Text expansionPrompt;
    [SerializeField] private int expansionPromptTime = 5;
    private bool fadeExpansionPrompt;
    
    private Quaternion currentRotation;
    private Quaternion targetRotation;
    private float stepSize;
    
    [SerializeField] private Sprite buttonSprite;
    [SerializeField] private Sprite buttonSpritePressed;
    [SerializeField] private TMPro.TMP_Text buttonText;
    public static event System.Action switchLoopEvent;
    private bool switchLoop = false;
    
    [SerializeField] private GameObject victoryScreen;
    

    private void OnEnable()
    {
        Player.onSwitchedLoopEvent += OnPlayerSwitchedLoop;
        Player.onHealthChangedEvent += UpdateHealth;
        Player.TurnSunStoneEvent += OnPlayerTurnSunStone;
        TimeManager.afterTimeResetEvent += OnAfterTimeReset;
        GardenManager.onGardenExpansion += OnGardenExpansion;
    }
    
    private void OnDisable()
    {
        Player.onSwitchedLoopEvent -= OnPlayerSwitchedLoop;
        Player.onHealthChangedEvent -= UpdateHealth;
        Player.TurnSunStoneEvent -= OnPlayerTurnSunStone;
        TimeManager.afterTimeResetEvent -= OnAfterTimeReset;
        GardenManager.onGardenExpansion -= OnGardenExpansion;
    }

    public void OnSwitchLoopPressed()
    {
        if (switchLoop) return;
        switchLoopEvent?.Invoke();
        switchLoopButton.GetComponent<Image>().sprite = buttonSpritePressed;
        switchLoop = true;
    }
    
    public void OnPlayerSwitchedLoop()
    {
        switchLoopButton.GetComponent<Image>().sprite = buttonSprite;
        switchLoop = false;
    }
    
    public void UpdateHealth(float healthValue)
    {
        for (int i = 0; i < health.Length; i++)
        {
            if (i < healthValue)
            {
                health[i].enabled = true;
            }
            else
            {
                health[i].enabled = false;
            }
        }
    }
    
    public void OnPlayerTurnSunStone(int wait)
    {
        sunStoneTurned = true;
        CalculateSunStoneRotationStep(wait, 0f);
    }
    
    public void OnAfterTimeReset()
    {
        sunStoneTurned = false;
        CalculateSunStoneRotationStep(TimeManager.staticStartTime, 180f);
    }

    private void Start()
    {
        CalculateSunStoneRotationStep(TimeManager.staticStartTime, 180f);
        expansionPrompt.color = Color.clear;
        expansionPrompt.gameObject.SetActive(true);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnSwitchLoopPressed();
        }
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMainMenu();
        }
        
        sunPivot.transform.rotation = Quaternion.RotateTowards(sunPivot.transform.rotation, targetRotation, stepSize * Time.deltaTime);
        
        if (fadeExpansionPrompt)
        {
            expansionPrompt.color = new Color(expansionPrompt.color.r, expansionPrompt.color.g, expansionPrompt.color.b, expansionPrompt.color.a - 0.005f);
            if (expansionPrompt.color.a <= 0.001f)
            {
                fadeExpansionPrompt = false;
            }
        }
    }

    void CalculateSunStoneRotationStep(int totalTime, float targetRotationAngle)
    {
        targetRotation = Quaternion.Euler(0, 0, targetRotationAngle); // Target rotation

        // Calculate the angle difference between current and target rotations
        currentRotation = sunPivot.transform.rotation;
        float angleDifference = Quaternion.Angle(currentRotation, targetRotation);

        // Calculate the step size (p)
        stepSize = angleDifference / totalTime;
    }
    
    public void OnGardenExpansion(GameObject ignore)
    {
        Debug.Log("Garden expanded");
        expansionPrompt.color = Color.white;
        StartCoroutine(HideExpansionPrompt());
    }
    
    IEnumerator HideExpansionPrompt()
    {
        yield return new WaitForSeconds(expansionPromptTime);
        fadeExpansionPrompt = true;
    }
    
    public void CloseTutorial()
    {
        TutorialManager.instance.CloseTutorial();
    }
    
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
