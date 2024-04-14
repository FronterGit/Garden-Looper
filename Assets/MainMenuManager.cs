using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject optionsMenu;
    
    public static event System.Action toggleTutorialEvent;
    
    private void Start()
    {
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
        optionsMenu.SetActive(false);
        
        AudioManager.instance.musicSource.Play();
        toggleTutorialEvent?.Invoke();
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
        AudioManager.instance.PlaySound("Continue");
    }
    
    public void ShowCredits()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
        AudioManager.instance.PlaySound("Continue");
    }
    
    public void ShowOptions()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        AudioManager.instance.PlaySound("Continue");
    }
    
    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
        optionsMenu.SetActive(false);
        AudioManager.instance.PlaySound("Continue");
    }
    
    public void ToggleTutorial()
    {
        toggleTutorialEvent?.Invoke();
    }
}
