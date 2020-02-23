using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class VRClient: MonoBehaviour
{
    
    public GameObject network;
    NetworkManager manager;
    public bool done = false;

    //To automatically connect to a server started from the PC side 
    void Start(){
        manager = network.GetComponent<NetworkManager>();
    }

    void Update(){
        if(!NetworkClient.isConnected){
        SetUpClient();
        }
    }

    // Update is called once per frame
    public void SetUpClient(){
        manager.StartClient();
        // client ready
            if (NetworkClient.isConnected && !ClientScene.ready)
            {
                if (GUILayout.Button("Client Ready"))
                {
                    ClientScene.Ready(NetworkClient.connection);

                    if (ClientScene.localPlayer == null)
                    {
                        ClientScene.AddPlayer();
                    }
                }
            }
    }
}
