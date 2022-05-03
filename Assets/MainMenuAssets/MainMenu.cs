using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class MainMenu : MonoBehaviour
{

    [SerializeField]
    NetworkManager manager;

    void Awake() {
        //manager = GetComponent<NewNetworkManager>();
    }

    void SetIP(string newIP) {
        manager.networkAddress = newIP;
    }
    
    public void HostGame() {
        manager.StartHost();
    }

    public void JoinGame() {
        if(manager.networkAddress == string.Empty) manager.networkAddress = "localhost";
        manager.StartClient();
    }

    public void CancelHost() {
        manager.StopHost();
    }

    public void CancelJoin() {
        manager.StopClient();
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
        SceneManager.LoadScene("MainMenu");
    }

}
