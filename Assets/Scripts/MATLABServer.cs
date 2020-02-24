using UnityEngine;
using System.Net.Sockets;
using System;
using System.IO;
using System.Collections;
using System.Globalization;
using Mirror;


public class MATLABServer : NetworkBehaviour {
      // Use this for initialization
      TcpListener listener;
      String msg;
      //Variables to reconstruct from the message
      public float[] joints = new float[7];
      public float[] delta_joints = new float[7];
      public int inv_speed = 5; 
      [SyncVar] private GameObject tg1, tg2, tg3, tg4, tg5, tg6, tg7;  

      void Start () {
          listener = new TcpListener (6321);
          listener.Start ();
          print ("The MATLAB server is listening");
          //Initializing the robot pieces to move via the IEnumerator function 
          //They are not spawned @ the start of the scene: can't be accessed here
      }
      // Update is called once per frame
      void Update () {
          if (!listener.Pending ())
          {
          } 
          //message handling goes here
          else 
          {
              print ("Socket comes");
              TcpClient client = listener.AcceptTcpClient ();
              NetworkStream ns = client.GetStream ();
              StreamReader reader = new StreamReader (ns);
              FindKUKAPieces();
              msg = reader.ReadToEnd();
              print (msg);
              //trying to acces GameObjects here
              

              joints = ReconstructJoints(msg);
              StartCoroutine(MoveKuka(joints));
          }
      }
    //Function to access the GameObjects to be moved
    private void FindKUKAPieces(){
        tg1 = GameObject.Find("Ring1");
        tg2 = GameObject.Find("Ring2");
        tg3 = GameObject.Find("Ring3");
        tg4 = GameObject.Find("Ring4");
        tg5 = GameObject.Find("Ring5");
        tg6 = GameObject.Find("Head");
        tg7 = GameObject.Find("Camera");
    }
    //Function to reconstruct the joint values from the string received from MATLAB 
    private float[] ReconstructJoints(String data){
        String[] joints_str = data.Split('#');
        float[] joints = new float[7]; 
        for (int i =0; i < joints_str.Length; i++){
        joints[i] = float.Parse(joints_str[i], CultureInfo.InvariantCulture.NumberFormat);
        joints[i] = Convert.ToSingle(joints[i] * (180.0 / 3.1415));
        //Debug.Log(joints[i]);
        }
        
        return joints;
    }



      //Function to continuously move the KUKA robot upon receiving the message 
     IEnumerator MoveKuka(float[] joints) {
        Debug.Log("Moving Kuka");
        for (int i=0; i < joints.Length; i++){
        delta_joints[i] = joints[i]/inv_speed;  
        }
        
        for (int i=0; i < inv_speed; i++){
        tg1.transform.Rotate(0f, -delta_joints[0], 0f);
        tg2.transform.Rotate(0f, delta_joints[1], 0f);
        tg3.transform.Rotate(0f, -delta_joints[2], 0f);
        tg4.transform.Rotate(0f, -delta_joints[3], 0f);
        tg5.transform.Rotate(0f, -delta_joints[4], 0f);
        tg6.transform.Rotate(0f, delta_joints[5], 0f);
        tg7.transform.Rotate(0f, delta_joints[6], 0f);
        //Debug.Log(tg7.transform.position);
        yield return null;
        }
        Debug.Log("Finished");
       
    }
  }