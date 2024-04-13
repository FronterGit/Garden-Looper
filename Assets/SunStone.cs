using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunStone : MonoBehaviour
{
    private Animator animator;
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
        animator = GetComponent<Animator>();
    }
    
    void OnPlayerTurnSunStone(int wait)
    {
        animator.SetTrigger("turn");
    }
}
