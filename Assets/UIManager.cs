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
    
    public static event System.Action switchLoopEvent;

    private void OnEnable()
    {
        Player.onSwitchedLoopEvent += OnPlayerSwitchedLoop;
        Player.onHealthChangedEvent += UpdateHealth;
    }
    
    private void OnDisable()
    {
        Player.onSwitchedLoopEvent -= OnPlayerSwitchedLoop;
        Player.onHealthChangedEvent -= UpdateHealth;
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
}
