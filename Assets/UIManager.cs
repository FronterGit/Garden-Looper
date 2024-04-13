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
    
    public static event System.Action switchLoopEvent;

    private void OnEnable()
    {
        Player.onSwitchedLoopEvent += OnPlayerSwitchedLoop;
    }
    
    private void OnDisable()
    {
        Player.onSwitchedLoopEvent -= OnPlayerSwitchedLoop;
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
}
