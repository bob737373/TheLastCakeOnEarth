using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.SceneManagement; 
using Mirror;

public class DeathButton : MonoBehaviour
{
    public void ReturnToMenu()
    {
        // if(NetworkClient.isConnected) {
        //     if(NetworkServer.active) {
        //         NetworkManager.singleton.StopHost();
        //     } else {
        //         NetworkManager.singleton.StopClient();
        //     }
        // }
        NetworkManager.singleton.offlineScene = "MainMenu";
        SceneManager.LoadScene("MainMenu");
    }
}
