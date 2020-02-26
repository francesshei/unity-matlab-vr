using UnityEngine;
using Mirror;

//NetworkBehaviour  instead of MonoBehaviour to handle the networking system and actions
public class SetUpLocalPlayer : NetworkBehaviour
{
    //public Camera playerCam;
    public GameObject EECam; 
    public GameObject Cam;
    //[SyncVar] bool ee; 
    private Camera EECamera;
    public Camera MainCamera;
    private Transform fixedCamera; 
    private Transform BECamera; 
    public Transform target; 
    public bool moveAround;
    // Start is called before the first frame update
    void Start()
    {   //If the client connects to the server, then the camera will be set to its position and put in its hierarchy 
        if(isLocalPlayer){
            Camera.main.transform.position = this.transform.position;
            Camera.main.transform.parent = this.transform;
            MainCamera = Camera.main; 
        }
    }

    /*void FindCameras(){
        EECam = GameObject.Find("Camera");
        Cam = GameObject.Find("CamerasInteraction");
        EECamera = EECam.GetComponent<Camera>();
        //Debug.Log(Cam.GetComponent<CamerasInteraction>());
        //Debug.Log(EECam);
    }*/

    // Update is called once per frame
    void Update()
    {
        //FindCameras(); 

        if(isLocalPlayer){
            fixedCamera = GameObject.Find("FloatingCamera").GetComponent<Transform>();
            BECamera = GameObject.Find("Camera").GetComponent<Transform>();
            target = GameObject.Find("Rene").GetComponent<Transform>();
            if (BECamera.eulerAngles.z >= 30f && BECamera.eulerAngles.z < 90.0f ){
            moveAround = true; 
            }
            else {
            moveAround = false;
            }
            if(moveAround){
            //Update the camera matrix
            Debug.Log("Rotating camera");
            fixedCamera.RotateAround(target.position, Vector3.up, 15 * Time.deltaTime);
            fixedCamera.LookAt(target); 
            }

            /*if(!GameObject.Find("CamerasInteraction").GetComponent<CamerasInteraction>().ee){
                Debug.Log("Changing camera");
                Camera.main.transform.position = EECamera.transform.position;
                Camera.main.transform.rotation = EECamera.transform.rotation;
                Camera.main.transform.parent = EECamera.transform;
            }
            else{
                Camera.main.transform.position = this.transform.position;
                Camera.main.transform.parent = this.transform;
            }

            //Debug.Log(GameObject.Find("CamerasInteraction").GetComponent<CamerasInteraction>().ee);
            MainCamera.enabled = GameObject.Find("CamerasInteraction").GetComponent<CamerasInteraction>().ee; 
            EECamera.enabled = !GameObject.Find("CamerasInteraction").GetComponent<CamerasInteraction>().ee; */
        }
    }
}
