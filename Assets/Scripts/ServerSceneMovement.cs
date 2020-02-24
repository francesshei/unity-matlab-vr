using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

//NetworkBehaviour  instead of MonoBehaviour to handle the networking system and actions
public class ServerSceneMovement : NetworkBehaviour
{
    void Start()
    {
        //If the client connects to the server, then the camera will be set to its position and put in its hierarchy 
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Move(10f));
    }
    IEnumerator Move(float angle) {
        this.transform.Rotate(0f,angle,0f);
        yield return null;
        }
       
}

