using UnityEngine;
using System.Net.Sockets;
using System;
using System.IO;

public class MATLABServer : MonoBehaviour {
      // Use this for initialization
      TcpListener listener;
      String msg;
      void Start () {
          listener = new TcpListener (6321);
          listener.Start ();
          print ("The MATLAB server is listening");
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
              msg = reader.ReadToEnd();
              print (msg);
          }
      }
  }