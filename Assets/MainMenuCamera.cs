using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    private Transform cameraTarget;
    private Camera mainCamera;
    private float speed = 0.03f;
    [SerializeField] private Player player;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        PickRandomChild();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //1% chance per frame for the player switchloop to be turned on
        if (Random.Range(0, 100) == 0)
        {
            player.switchLoop = true;
        }
        
        //Lerp main camera to target
        mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, cameraTarget.position, speed);
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, -10);
        
        //if camera has reached target, pick a new target
        if (Vector3.Distance(mainCamera.transform.position, cameraTarget.position) < 12f)
        {
            PickRandomChild();
        }
    }
    
    void PickRandomChild()
    {
        int randomIndex = Random.Range(0, transform.childCount);
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == randomIndex)
            {
                if (transform.GetChild(i) == cameraTarget)
                {
                    PickRandomChild();
                    return;
                }
                cameraTarget = transform.GetChild(i);
            }
        }
    }
}
