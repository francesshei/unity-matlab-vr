using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class CamerasInteraction : NetworkBehaviour
{
    public Camera EECamera;    
    public Camera EECamera2;    
    private Camera MainCamera; 
    [SyncVar] public bool ee =  true; 

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = Camera.main; 
        //To set the server camera properly
        Camera.main.enabled = true; 
        EECamera.enabled = false;
        EECamera2.enabled = false; 

    }
    public void ChangeEE()
     {
        if(Input.GetKeyDown("space")){
           ee = !ee;
           Debug.Log(ee);
        }
     }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")){
           ee = !ee;
           Debug.Log(ee);
        }
        //MainCamera.enabled = ee; 
        //EECamera.enabled = !ee;
    }
}
