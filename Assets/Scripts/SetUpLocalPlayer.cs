using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

//NetworkBehaviour  instead of MonoBehaviour to handle the networking system and actions
public class SetUpLocalPlayer : NetworkBehaviour
{
    //public Camera playerCam;
    public GameObject VRCamera;
    // Start is called before the first frame update
    void Start()
    {
        //If the client connects to the server, then the camera will be set to its position and put in its hierarchy 
        if(isLocalPlayer){
            Camera.main.transform.position = this.transform.position;
            Camera.main.transform.parent = this.transform;
        }

    }

    // Update is called once per frame
    void Awake()
    {
        //VRCamera = GameObject.Find("VRCamera");
    }
}
