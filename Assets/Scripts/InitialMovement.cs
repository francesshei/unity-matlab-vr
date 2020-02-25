using UnityEngine;
using System.Net.Sockets;
using System;
using System.IO;
using System.Collections;
using System.Globalization;
using Mirror;


public class InitialMovement : NetworkBehaviour {
      // Use this for initialization
      public float[] initJoints = new float[7];
      public float[] initSecondJoints = new float[7];
      private string initData;
      private string initSecondData;
      public float[] delta_joints = new float[7];
      public float[] second_delta_joints = new float[7];
      private bool moved = false; 
      public int inv_speed = 5; 
      [SyncVar] private GameObject b1, tg1, tg2, tg3, tg4, tg5, tg6, tg7, t1, k;  
      [SyncVar] private GameObject b2, tg8, tg9, tg10, tg11, tg12, tg13, tg14, t2;  

      void Start () {
          print ("Going to home position");
         //joints = ReconstructJoints(msg);
         //StartCoroutine(MoveKuka(joints));
          //FindKUKAPieces();
          initialMovement();
          //Initializing the robot pieces to move via the IEnumerator function 
          //They are not spawned @ the start of the scene: can't be accessed here
      }
      // Update is called once per frame
      void Update () {
          
      }
    //Function to access the GameObjects to be moved
    private void FindKUKAPieces(){
        b1 = GameObject.Find("Ring0");
        tg1 = GameObject.Find("Ring1");
        tg2 = GameObject.Find("Ring2");
        tg3 = GameObject.Find("Ring3");
        tg4 = GameObject.Find("Ring4");
        tg5 = GameObject.Find("Ring5");
        tg6 = GameObject.Find("Head");
        tg7 = GameObject.Find("Camera");
        t1 = GameObject.Find("Scalpel");

        k = GameObject.Find("Rene");

        b2 = GameObject.Find("Ring02");
        tg8 = GameObject.Find("Ring12");
        tg9 = GameObject.Find("Ring22");
        tg10 = GameObject.Find("Ring32");
        tg11 = GameObject.Find("Ring42");
        tg12 = GameObject.Find("Ring52");
        tg13 = GameObject.Find("Head2");
        tg14 = GameObject.Find("Camera2");
        t2 = GameObject.Find("Scalpel2");
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

    private void initialMovement(){
        initData = "0#0.7660#-3.1416#1.0554#0#-1.3013#-3.1416";
        initSecondData = "3.1416#0.7660#-3.1416#1.0554#0#-1.3013#-3.1416";
        initJoints = ReconstructJoints(initData);
        initSecondJoints = ReconstructJoints(initSecondData);
        FindKUKAPieces();
        DisplayPosition();
        //StartCoroutine(MoveKuka(initJoints));
        //StartCoroutine(MoveSecondKuka(initSecondJoints));

    }

    private void DisplayPosition(){
        Debug.Log("Base-Shoulder");
        Debug.Log(b1.transform.position);
        Debug.Log(tg2.transform.position);
        Debug.Log("Shoulder-Elbow");
        Debug.Log(tg4.transform.position);
        Debug.Log("Elbow-Wrist");
        Debug.Log(tg7.transform.position);
        Debug.Log("Wrist-Tool");
        Debug.Log(t1.transform.position);
        Debug.Log("Kidney");
        Debug.Log(k.transform.position);
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
        Debug.Log("Finished");
       
    }
  }
