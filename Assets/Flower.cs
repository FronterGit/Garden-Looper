using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite graveSprite;
    public bool dead;

    public event Action<Flower> onDeath;

    private void OnEnable()
    {
        TimeManager.stepEvent += Step;
    }
    
    private void OnDisable()
    {
        TimeManager.stepEvent -= Step;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = maxHealth;
        
        if(FlowerManager.instance != null) FlowerManager.instance.AddFlower(this);
    }
    
    void Step()
    {
        if (dead) return;
        
        //Step the health of the flower
        health--;
        
        //If the health is 0 or less, destroy the flower
        if (health <= 0)
        {
            Debug.Log("Flower has died");
            onDeath?.Invoke(this);
            spriteRenderer.sprite = graveSprite;
            SetColorBack();
            dead = true;
            transform.localScale *= 0.85f;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<ParticleSystem>().Stop();
            return;
        }
        
        //Change the color of the flower based on its health
        float hColor;
        float sColor;
        float vColor;
        Color.RGBToHSV(spriteRenderer.color, out hColor, out sColor, out vColor);
        Color newColor = Color.HSVToRGB(hColor, sColor, health / maxHealth, false);
        spriteRenderer.color = newColor;
    }
    
    public void SetColorBack()
    {
        spriteRenderer.color = Color.white;
    }

    public void GetWatered()
    {
        float hColor;
        float sColor;
        float vColor;
        Color.RGBToHSV(spriteRenderer.color, out hColor, out sColor, out vColor);
        Color newColor = Color.HSVToRGB(hColor, sColor, health / maxHealth, false);
        spriteRenderer.color = newColor;
    }
}
