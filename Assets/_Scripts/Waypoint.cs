using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Transform baseNextWaypoint;
    public Transform newNextWaypoint;
    public bool isStartWaypoint;
    public bool isActiveWaypoint;

    private void OnEnable()
    {
        Player.activeWaypointEvent += ActivateWaypoint;
    }
    
    private void OnDisable()
    {
        Player.activeWaypointEvent -= ActivateWaypoint;
    }
    
    private void ActivateWaypoint(Waypoint waypoint)
    {
        if (waypoint == this)
        {
            isActiveWaypoint = true;
        }
        else
        {
            isActiveWaypoint = false;
        }
    }
}
