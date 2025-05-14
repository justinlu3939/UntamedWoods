using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ON further review, it seems this script is unnecessary

/*
    Readme
    1. add "MainCamera" tag to the camera
    2. add this script to the mouse cursor
        - add the mouse cursor under the player so that it is always on screen
*/
/*
    Purpose - build an in game mouse cursor that follows the mouse position
    1.  the cursor will move around the screen and allow players to interact with 
        close objects
    2.  the purpose is to allow for the player to click on the settings menu
*/

public class MouseAim : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //this code just rotates the cursor around the player but doesnt actually move along the mouse cursor
        // mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        // Vector3 rotation = mousePos - transform.position;
        // float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg; //this creates the rotation
        // transform.rotation = Quaternion.Euler(0, 0, rotZ);

        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Ensure the cursor stays on the same Z plane
        transform.position = mousePos;
    }
}