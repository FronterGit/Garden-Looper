using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float step;
    public int startTime;
    public static int staticStartTime;
    public static int time;
    public bool ticking = true;

    public static event System.Action stepEvent;
    public static event System.Action afterTimeResetEvent;
    public static event System.Action timeUpEvent;

    private void OnEnable()
    {
        Player.TurnSunStoneEvent += OnPlayerTurnSunStone;
        GardenManager.victoryEvent += () => ticking = false;
        Player.defeatEvent += () => ticking = false;
    }
    
    private void OnDisable()
    {
        GardenManager.victoryEvent -= () => ticking = false;
        Player.defeatEvent -= () => ticking = false;
        Player.TurnSunStoneEvent -= OnPlayerTurnSunStone;
    }

    void Awake()
    {
        time = startTime;
        staticStartTime = startTime;
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
                timeUpEvent?.Invoke();
            }
            yield return new WaitForSeconds(step);
            stepEvent?.Invoke();
        }
    }
    void OnPlayerTurnSunStone(int wait)
    {
        ticking = false;
        StartCoroutine(ResetTime(wait));
    }
    
    IEnumerator ResetTime(int wait)
    {
        //Wait for the player to turn the sun stone
        yield return new WaitForSeconds(wait);
        
        //Reset the time
        time = startTime;
        
        //For debug purposes
        staticStartTime = startTime;
        
        //Invoke the afterTimeResetEvent
        afterTimeResetEvent?.Invoke();
        
        //Start the time again
        ticking = true;
        StartCoroutine(Step());
    }
}
