using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float step;
    public int time;
    public bool ticking = true;

    public static event System.Action decayEvent;

    private void OnEnable()
    {
        Player.TurnSunStoneEvent += OnPlayerTurnSunStone;
    }
    
    private void OnDisable()
    {
        Player.TurnSunStoneEvent -= OnPlayerTurnSunStone;
    }

    void Start()
    {
        StartCoroutine(Step());
    }
    
    IEnumerator Step()
    {
        while (ticking)
        {
            time--;
            if(time <= 0)
            {
                Debug.Log("Time's up!");
            }
            yield return new WaitForSeconds(step);
            decayEvent?.Invoke();
        }
    }
    void OnPlayerTurnSunStone(int wait)
    {
        ticking = false;
        StartCoroutine(ResetTime(wait));
    }
    
    IEnumerator ResetTime(int wait)
    {
        yield return new WaitForSeconds(wait);
        time = 100;
        ticking = true;
        
        StartCoroutine(Step());
    }
}
