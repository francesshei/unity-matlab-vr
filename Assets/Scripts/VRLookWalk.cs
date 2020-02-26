using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRLookWalk : MonoBehaviour
{
    public Transform target;
    public float toggleAngle = 45.0f;
    public float speed = 1.5f; 
    public bool moveAround;

    //private CharacterController cc;  
    // Start is called before the first frame update
    void Start() 
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.eulerAngles.z >= toggleAngle && this.transform.eulerAngles.z < 90.0f ){
            moveAround = true; 
        }
        else {
            moveAround = false;
        }
        if(moveAround){
            //Update the camera matrix
            this.transform.RotateAround(target.position, Vector3.up, 15 * Time.deltaTime);
            this.transform.LookAt(target); 
        }
    }
}
