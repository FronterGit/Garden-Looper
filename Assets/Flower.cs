using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public SpriteRenderer spriteRenderer;

    public event Action<Flower> onDeath;

    private void OnEnable()
    {
        TimeManager.decayEvent += Decay;
    }
    
    private void OnDisable()
    {
        TimeManager.decayEvent -= Decay;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = maxHealth;
        
        FlowerManager.instance.AddFlower(this);
    }
    
    void Decay()
    {
        //Decay the health of the flower
        health--;
        
        //If the health is 0 or less, destroy the flower
        if (health <= 0)
        {
            Debug.Log("Flower has died");
            onDeath?.Invoke(this);
            Destroy(gameObject);
        }

        //TODO: Flower's color does not change when it's healed.
        //Change the color of the flower based on its health
        float hColor;
        float sColor;
        float vColor;
        Color.RGBToHSV(spriteRenderer.color, out hColor, out sColor, out vColor);
        Color newColor = Color.HSVToRGB(hColor, sColor, health / maxHealth, false);
        spriteRenderer.color = newColor;
    }
}
