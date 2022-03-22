using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author:  Benjamin Boese
//Date: 11-6-2019
//Purpose: For making a UI object in world space look at the camera took the info from the Unity API
public class LookAtCamera : MonoBehaviour
{
    [Header("For reference only. should show the camera tagged as main camera")]
    public Camera uiCamera;

    private void Awake()
    {
        uiCamera = Camera.main;//setting the main camera to the uicamera at start
    }
    // Update is called once per frame
    void Update()//updating the world space UI canvas every frame
    {
        transform.LookAt(transform.position + uiCamera.transform.rotation * Vector3.forward, uiCamera.transform.rotation * Vector3.up);
    }


}
