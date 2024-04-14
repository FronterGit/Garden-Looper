using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenManager : MonoBehaviour
{
    [SerializeField] private List<Expansion> expansions;
    public static event System.Action<GameObject> onGardenExpansion;

    [SerializeField] private int index;
    [SerializeField] private int stepCount;
    
    // Start is called before the first frame update
    private void OnEnable()
    {
        TimeManager.stepEvent += OnStep;
    }
    
    private void OnDisable()
    {
        TimeManager.stepEvent -= OnStep;
    }

    private void Start()
    {
        index = -1;
        stepCount = 0;
        
        stepCount = expansions[0].stepCount;
    }

    void OnStep()
    {
        stepCount--;
        if (stepCount <= 0)
        {
            Debug.Log("Expanding garden");
            index++;
            if (index >= expansions.Count)
            {
                Debug.Log("Game Over");
                return;
            }
            stepCount = expansions[index].stepCount;
            
            expansions[index].loop.SetActive(true);
            
            expansions[index].flowers.SetActive(true);
            
            expansions[index].tilemap.SetActive(true);
            
            onGardenExpansion?.Invoke(expansions[index].cameraBounds);
        }
    }
}

[System.Serializable]
class Expansion
{
    public GameObject loop;
    public GameObject flowers;
    public GameObject cameraBounds;
    public GameObject tilemap;
    public int stepCount;
}
