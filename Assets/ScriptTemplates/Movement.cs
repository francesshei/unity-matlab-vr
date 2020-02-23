using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Movement : MonoBehaviour
{
    public float toggleAngle = 30.0f;
    public float speed = 3.0f; 
    public bool moveForward;
    private Transform vrCamera;
    private CharacterController cc;  
    // Start is called before the first frame update
    void Start() 
    {
        vrCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
        cc = GetComponent<CharacterController>();   
        transform.position = new Vector3 (0f,0f,0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            //transform.Rotate(0.0f, 30.0f, 0.0f);
            moveForward = true; }
        else{
            moveForward = false;
        }

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
