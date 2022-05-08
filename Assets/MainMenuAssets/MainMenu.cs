using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField]
    NetworkManager manager;

    InputField ip;

    void Awake() {
        //manager = GetComponent<NewNetworkManager>();
    }

    void Start() {
        ip = GetComponentInChildren<InputField>();
    }

    public void SetIP() {
        Debug.Log("ip changed to " + ip.text);
        NetworkManager.singleton.networkAddress = ip.text;
    }
    
    public void HostGame() {
        manager.StartHost();
    }

    public void JoinGame() {
        Debug.Log("passed in: " + manager.networkAddress);
        if(NetworkManager.singleton.networkAddress == string.Empty) NetworkManager.singleton.networkAddress = "localhost";
        NetworkManager.singleton.StartClient();
    }

    public void CancelHost() {
        NetworkManager.singleton.StopHost();
    }

    public void CancelJoin() {
        NetworkManager.singleton.StopClient();
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("LoadScene");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void ReturnToMain()
    {
        if(NetworkServer.active && NetworkClient.isConnected) {
            NetworkManager.singleton.StopHost();
        } else if(NetworkClient.isConnected) {
            NetworkManager.singleton.StopClient();
        } else if(manager) {
            NetworkManager.singleton.ServerChangeScene("MainMenu");
        } else {
            SceneManager.LoadScene("MainMenu");
        }
    }

}
