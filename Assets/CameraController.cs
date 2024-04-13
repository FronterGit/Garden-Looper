using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool transforming;
    private Vector2 direction;
    [SerializeField] private float factor = 1f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction += Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            direction += Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction += Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction += Vector2.right;
        }
        
        if (Input.GetKeyUp(KeyCode.W))
        {
            direction -= Vector2.up;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            direction -= Vector2.left;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            direction -= Vector2.down;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            direction -= Vector2.right;
        }
        
        //If no key is pressed, reset direction
        if (!Input.anyKey)
        {
            direction = Vector2.zero;
        }
        
        
    }

    private void FixedUpdate()
    {
        transform.position += (Vector3)direction * factor;
    }
}
