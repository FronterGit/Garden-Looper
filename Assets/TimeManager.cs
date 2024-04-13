using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float step;

    public static event System.Action decayEvent;

    void Start()
    {
        StartCoroutine(Step());
    }
    
    IEnumerator Step()
    {
        while (true)
        {
            yield return new WaitForSeconds(step);
            decayEvent?.Invoke();
        }
    }
}
