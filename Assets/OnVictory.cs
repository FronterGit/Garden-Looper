using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OnVictory : MonoBehaviour
{
    [SerializeField] private Image victoryBackground;
    private bool fadeVictoryBackground;
    [SerializeField] private TMP_Text victoryText1;
    private bool fadeVictoryText1;
    [SerializeField] private TMP_Text victoryText2;
    private bool fadeVictoryText2;
    [SerializeField] private Button victoryButton;
    private bool fadeVictoryButton;

    void Start()
    {
        victoryBackground.gameObject.SetActive(true);
        victoryBackground.color = Color.clear;
        victoryText1.gameObject.SetActive(true);
        victoryText1.color = Color.clear;
        victoryText2.gameObject.SetActive(true);
        victoryText2.color = Color.clear;
        victoryButton.gameObject.SetActive(true);
        victoryButton.image.color = Color.clear;
        fadeVictoryBackground = true;
    }

    private void Update()
    {
        if (fadeVictoryBackground)
        {
            victoryBackground.color = new Color(victoryBackground.color.r, victoryBackground.color.g,
                victoryBackground.color.b, victoryBackground.color.a + Time.deltaTime * 0.5f);
            if (victoryBackground.color.a >= 0.99f)
            {
                victoryBackground.color = Color.black;
                fadeVictoryBackground = false;
                fadeVictoryText1 = true;
            }
        }
        else if (fadeVictoryText1)
        {
            victoryText1.color = Color.Lerp(victoryText1.color, Color.white, Time.deltaTime);
            if (victoryText1.color.a >= 0.99f)
            {
                fadeVictoryText1 = false;
                fadeVictoryText2 = true;
            }
        }
        else if (fadeVictoryText2)
        {
            victoryText2.color = Color.Lerp(victoryText2.color, Color.white, Time.deltaTime);
            if (victoryText2.color.a >= 0.99f)
            {
                fadeVictoryText2 = false;
                fadeVictoryButton = true;
            }
        }
        else if (fadeVictoryButton)
        {
            victoryButton.image.color = Color.Lerp(victoryButton.image.color, Color.white, Time.deltaTime);
            if (victoryButton.image.color.a >= 0.99f)
            {
                fadeVictoryButton = false;
            }
        }
    }
}
