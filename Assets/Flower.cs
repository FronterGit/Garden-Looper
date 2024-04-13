using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public float health;
    public float maxHealth;

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
        health = maxHealth;
    }
    
    void Decay()
    {
        health--;
        if (health <= 0)
        {
            Debug.Log("Flower has died");
            Destroy(gameObject);
        }
    }
    
    
}
