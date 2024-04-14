using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool transforming;
    private Vector2 direction;
    [SerializeField] private float factor = 1f;
    [SerializeField] private bool border;
    
    [SerializeField] public GameObject walls;

    private void OnEnable()
    {
        GardenManager.onGardenExpansion += OnGardenExpansion;
    }
    
    private void OnDisable()
    {
        GardenManager.onGardenExpansion -= OnGardenExpansion;
    }

    void Update()
    {
        //If no key is pressed, reset direction
        if (!Input.anyKey)
        {
            border = false;
            direction = Vector2.zero;
        }
        
        if(border) return;
        
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
        
        transform.position += (Vector3)direction * (factor * Time.deltaTime);
        
        //Zoom out when mouse wheel is scrolled down
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.orthographicSize >= 9) return;
            Camera.main.orthographicSize += 1;
        }
        
        //Zoom in when mouse wheel is scrolled up
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.orthographicSize <= 6) return;
            Camera.main.orthographicSize -= 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            border = true;
        }
    }

    void OnGardenExpansion(GameObject cameraBounds)
    {
        Destroy(walls);
        
        walls = cameraBounds;
        
        walls.SetActive(true);
    }

}
