using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CustomNetworkManager : NetworkManager
{
    public override void OnStartServer() {
        Debug.Log("Server start");
    }

    public override void OnStopServer()
    {
        Debug.Log("Server stopped");
    }

    public override void OnClientConnect()
    {
        Debug.Log("Connected to Server!");
    }

    public override void OnClientDisconnect() {
        Debug.Log("Disconnected from Server!");
    }
}
