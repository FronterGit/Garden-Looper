using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerManager : MonoBehaviour
{
    public static FlowerManager instance;
    public List<Flower> flowers = new List<Flower>();
    
    public static event System.Action<float> flowerDeathEvent;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    
    public void AddFlower(Flower flower)
    {
        flowers.Add(flower);
        flower.onDeath += RemoveFlower;
    }
    
    public void RemoveFlower(Flower flower)
    {
        flowers.Remove(flower);
        flower.onDeath -= RemoveFlower;
        if(AudioManager.instance != null) AudioManager.instance.PlaySound("FlowerDeath");
        
        flowerDeathEvent?.Invoke(-1);
    }
}
