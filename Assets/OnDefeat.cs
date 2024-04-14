using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnDefeat : MonoBehaviour
{
    [SerializeField] private Image defeatBackground;
    private bool fadeDefeatBackground;

    [SerializeField] private TMPro.TMP_Text defeatText;
    private bool fadeDefeatText;
    
    [SerializeField] private Button defeatButton;
    private bool fadeDefeatButton;
    
    void Start()
    {
        defeatBackground.gameObject.SetActive(true);
        defeatBackground.color = Color.clear;
        defeatText.gameObject.SetActive(true);
        defeatText.color = Color.clear;
        defeatButton.gameObject.SetActive(true);
        defeatButton.image.color = Color.clear;
        fadeDefeatBackground = true;
        
        if(AudioManager.instance != null) AudioManager.instance.musicSource.Stop();
    }
    
    private void Update()
    {
        if (fadeDefeatBackground)
        {
            defeatBackground.color = new Color(defeatBackground.color.r, defeatBackground.color.g,
                defeatBackground.color.b, defeatBackground.color.a + Time.deltaTime * 0.5f);
            if (defeatBackground.color.a >= 0.99f)
            {
                defeatBackground.color = Color.black;
                fadeDefeatBackground = false;
                fadeDefeatText = true;
            }
        }
        else if (fadeDefeatText)
        {
            defeatText.color = Color.Lerp(defeatText.color, Color.white, Time.deltaTime);
            if (defeatText.color.a >= 0.99f)
            {
                fadeDefeatText = false;
                fadeDefeatButton = true;
            }
        }
        else if (fadeDefeatButton)
        {
            defeatButton.image.color = Color.Lerp(defeatButton.image.color, Color.white, Time.deltaTime);
            if (defeatButton.image.color.a >= 0.99f)
            {
                fadeDefeatButton = false;
            }
        }
    }
}
