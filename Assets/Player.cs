using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    //BUG: IF YOU SPAM THE SWITCH LOOP BUTTON, THE PLAYER WILL GET STUCK
    
    public float speed;
    public bool switchLoop;
    public Transform currentWaypoint;
    public Transform nextWaypoint;
    public Waypoint potentialWaypoint;
    
    public float healPower;
    
    public static event Action<Waypoint> activeWaypointEvent;
    public static event Action onSwitchedLoopEvent;

    private void OnEnable()
    {
        UIManager.switchLoopEvent += SwitchLoop;
    }
    
    private void OnDisable()
    {
        UIManager.switchLoopEvent -= SwitchLoop;
    }

    private void Start()
    {
        //Invoke the event to activate the first waypoint
        activeWaypointEvent?.Invoke(currentWaypoint.GetComponent<Waypoint>());
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, speed * Time.deltaTime);
        if (transform.position == currentWaypoint.position)
        {
            currentWaypoint = nextWaypoint;
        }
    }
    
    void SwitchLoop()
    {
        switchLoop = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Waypoint"))
        {
            //If hit a waypoint that is not the active waypoint
            if (!other.GetComponent<Waypoint>().isActiveWaypoint)
            {
                //If we don't want to switch or the waypoint is not a starting waypoint, ignore the waypoint
                if (!switchLoop || !other.GetComponent<Waypoint>().isStartWaypoint) return;
                currentWaypoint = other.transform;
                
                //Set it as potential waypoint
                if (potentialWaypoint == null) potentialWaypoint = other.GetComponent<Waypoint>();
                //If that waypoint is the start of a new loop
                if (potentialWaypoint.isStartWaypoint)
                {
                    nextWaypoint = potentialWaypoint.baseNextWaypoint;
                    //Invoke the event to activate the new base waypoint
                    activeWaypointEvent?.Invoke(potentialWaypoint.baseNextWaypoint.GetComponent<Waypoint>());
                }
                
                switchLoop = false;
                onSwitchedLoopEvent?.Invoke();
            }
            //If we hit a waypoint and it is the active waypoint
            else
            {
                //If we want to switch, set the next waypoint to the newNextWaypoint if it has one
                if (switchLoop && other.GetComponent<Waypoint>().newNextWaypoint != null)
                {
                    nextWaypoint = other.GetComponent<Waypoint>().newNextWaypoint;
                    activeWaypointEvent?.Invoke(other.GetComponent<Waypoint>().newNextWaypoint.GetComponent<Waypoint>());
                    switchLoop = false;
                    onSwitchedLoopEvent?.Invoke();
                }
                //If we don't want to switch, set the next waypoint to the baseNextWaypoint
                else
                {
                    nextWaypoint = other.GetComponent<Waypoint>().baseNextWaypoint;
                    activeWaypointEvent?.Invoke(other.GetComponent<Waypoint>().baseNextWaypoint.GetComponent<Waypoint>());
                }
            }
        }
        
        if (other.CompareTag("Flower"))
        {
            WaterFlower(other.GetComponent<Flower>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        potentialWaypoint = null;
    }
    
    void WaterFlower(Flower flower)
    {
        flower.health += healPower;
        if (flower.health > flower.maxHealth) flower.health = flower.maxHealth;
    }
}
