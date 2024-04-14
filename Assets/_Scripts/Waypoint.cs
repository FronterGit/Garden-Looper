using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Waypoint : MonoBehaviour
{
    public Transform baseNextWaypoint;
    public Transform newNextWaypoint;
    [FormerlySerializedAs("isStartWaypoint")] public bool isEntryWaypoint;
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
