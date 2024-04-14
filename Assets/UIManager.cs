using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button switchLoopButton;
    
    public Color switchLoopOnColor;
    public Color switchLoopOffColor;
    
    public Image[] health = new Image[3];
    public GameObject sunPivot;
    private float timeToTurn;
    private bool sunStoneTurned;
    
    private Quaternion currentRotation;
    private Quaternion targetRotation;
    private float stepSize;
    public static event System.Action switchLoopEvent;
    

    private void OnEnable()
    {
        Player.onSwitchedLoopEvent += OnPlayerSwitchedLoop;
        Player.onHealthChangedEvent += UpdateHealth;
        Player.TurnSunStoneEvent += OnPlayerTurnSunStone;
        TimeManager.afterTimeResetEvent += OnAfterTimeReset;
    }
    
    private void OnDisable()
    {
        Player.onSwitchedLoopEvent -= OnPlayerSwitchedLoop;
        Player.onHealthChangedEvent -= UpdateHealth;
        Player.TurnSunStoneEvent -= OnPlayerTurnSunStone;
        TimeManager.afterTimeResetEvent -= OnAfterTimeReset;
    }

    public void OnSwitchLoopPressed()
    {
        switchLoopEvent?.Invoke();
        switchLoopButton.GetComponent<Image>().color = switchLoopOnColor;
    }
    
    public void OnPlayerSwitchedLoop()
    {
        switchLoopButton.GetComponent<Image>().color = switchLoopOffColor;
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
    }

    private void Update()
    {
        sunPivot.transform.rotation = Quaternion.RotateTowards(sunPivot.transform.rotation, targetRotation, stepSize * Time.deltaTime);
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
}
