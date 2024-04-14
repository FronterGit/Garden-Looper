using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

namespace WorldTime
{
    [RequireComponent(typeof(Light2D))]
    public class WorldLight : MonoBehaviour
    {
        public int day_duration;

        [SerializeField] private Gradient gradient;
        private Light2D light;
        private float startTime;
        private float percentage;
        private float targetDayTime;
        private float currentTime;
        private float stepSize;

        private void OnEnable()
        {
            Player.TurnSunStoneEvent += OnPlayerTurnSunStone;
            TimeManager.afterTimeResetEvent += OnAfterTimeReset;
        }
        
        private void OnDisable()
        {
            Player.TurnSunStoneEvent -= OnPlayerTurnSunStone;
            TimeManager.afterTimeResetEvent -= OnAfterTimeReset;
        }

        private void Awake()
        {
            light = GetComponent<Light2D>();
            startTime = Time.time;
        }
        
        private void Start()
        {
            day_duration = TimeManager.staticStartTime;
            percentage = 0;
            CalculateDayTimeStep(day_duration, 1);
        }
        
        // Update is called once per frame
        void Update()
        {
            //Lerp a float between current time and target time
            percentage += stepSize * Time.deltaTime;
            
            light.color = gradient.Evaluate(percentage);
        }
        
        void OnPlayerTurnSunStone(int wait)
        {
            CalculateDayTimeStep(wait, 0);
        }
        
        void OnAfterTimeReset()
        {
            CalculateDayTimeStep(TimeManager.staticStartTime, 1);
        }

        void CalculateDayTimeStep(int totalTime, float target)
        {
            targetDayTime = target;

            // Calculate the angle difference between current and target rotations
            currentTime = percentage;
            float percentageDifference = targetDayTime - currentTime;
 
            // Calculate the step size (p)
            stepSize = percentageDifference / totalTime;
        }
    }
}
