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
      TcpListener secondListener;

      String msg;
      String secondmsg;
      String initData;
      //Variables to reconstruct from the message
      public float[] joints = new float[7];
      public float[] secondJoints = new float[7];
      public float[] delta_joints = new float[7];
      public float[] second_delta_joints = new float[7];
      private bool moved = false; 
      public int inv_speed = 5; 
      [SyncVar] private GameObject tg1, tg2, tg3, tg4, tg5, tg6, tg7;  
      [SyncVar] private GameObject tg8, tg9, tg10, tg11, tg12, tg13, tg14;

      void Start () {
          listener = new TcpListener (6321);
          secondListener = new TcpListener (8080);
          
          listener.Start ();
          secondListener.Start ();

          print ("The MATLAB server is listening");
          //FindKUKAPieces();
          //initialMovement();
          //Initializing the robot pieces to move via the IEnumerator function 
          //They are not spawned @ the start of the scene: can't be accessed here
      }
      // Update is called once per frame
      void Update () {
          if (!listener.Pending () && !secondListener.Pending ())
          {
          } 
          //message handling goes here
          else 
          {
              print ("Socket comes");

              TcpClient client = listener.AcceptTcpClient ();
              TcpClient secondclient = secondListener.AcceptTcpClient ();

              NetworkStream ns = client.GetStream ();
              NetworkStream sns = secondclient.GetStream ();

              StreamReader reader = new StreamReader (ns);
              StreamReader secondreader = new StreamReader (sns);
            

              FindKUKAPieces();

              msg = reader.ReadToEnd();
              //print (msg);

              secondmsg = secondreader.ReadToEnd();
              
              joints = ReconstructJoints(msg);
              secondJoints = ReconstructJoints(secondmsg);

              StartCoroutine(MoveKuka(joints));
              StartCoroutine(MoveSecondKuka(secondJoints));
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
        tg8 = GameObject.Find("Ring12");
        tg9 = GameObject.Find("Ring22");
        tg10 = GameObject.Find("Ring32");
        tg11 = GameObject.Find("Ring42");
        tg12 = GameObject.Find("Ring52");
        tg13 = GameObject.Find("Head2");
        tg14 = GameObject.Find("Camera2");
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
        //Debug.Log("Finished");
       
    }
    IEnumerator MoveSecondKuka(float[] joints) {
        Debug.Log("Moving Kuka");
        for (int i=0; i < joints.Length; i++){
        second_delta_joints[i] = joints[i]/inv_speed;  
        }
        
        for (int i=0; i < inv_speed; i++){
        tg8.transform.Rotate(0f, - second_delta_joints[0], 0f);
        tg9.transform.Rotate(0f,  second_delta_joints[1], 0f);
        tg10.transform.Rotate(0f, - second_delta_joints[2], 0f);
        tg11.transform.Rotate(0f, -second_delta_joints[3], 0f);
        tg12.transform.Rotate(0f, -second_delta_joints[4], 0f);
        tg13.transform.Rotate(0f, second_delta_joints[5], 0f);
        tg14.transform.Rotate(0f, second_delta_joints[6], 0f);
        //Debug.Log(tg7.transform.position);
        yield return null;
        }
        //Debug.Log("Finished");
       
    }
  }