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
    
    public void HostGame() {
        manager.StartHost();
    }

    public void JoinGame() {
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
