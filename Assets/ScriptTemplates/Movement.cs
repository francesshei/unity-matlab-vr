using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Movement : MonoBehaviour
{
    public float toggleAngle = 30.0f;
    public float speed = 3.0f; 
    public bool moveForward;
    public bool moveAround;
    public Transform target;
    private Transform vrCamera;
    private Transform fixedCamera; 
    private CharacterController cc;  
    // Start is called before the first frame update
    void Start() 
    {
        vrCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
        target = GameObject.Find("Rene").GetComponent<Transform>();
        fixedCamera = GameObject.Find("FloatingCamera").GetComponent<Transform>();
        cc = GetComponent<CharacterController>();   
        transform.position = new Vector3 (0f,0f,0f);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (fixedCamera.eulerAngles.z >= toggleAngle && fixedCamera.eulerAngles.z < 90.0f ){
            moveAround = true; 
        }
        else {
            moveAround = false;
        }
        Debug.Log(moveAround);
        if(!moveAround){
            //Update the camera matrix
            Debug.Log("Rotating camera");
            fixedCamera.RotateAround(target.position, Vector3.up, 15 * Time.deltaTime);
            fixedCamera.LookAt(target); 
        }*/

        if (vrCamera.eulerAngles.x >= toggleAngle && vrCamera.eulerAngles.x < 90.0f ){
            moveForward = true; 
        }
        else {
            moveForward = false;
        }
        if(moveForward){
            Vector3 forward = vrCamera.TransformDirection(Vector3.forward);
            cc.SimpleMove(forward * speed);    
        }
        
    }
}
