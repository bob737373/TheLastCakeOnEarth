using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        //Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        //Time.timeScale = 1f;
    }

    public void Home()//(int sceneID)
    {
        Time.timeScale = 1f;
        //Application.Quit();
        if(NetworkServer.active && NetworkClient.isConnected) {
            NetworkManager.singleton.StopHost();
        } else if(NetworkClient.isConnected) {
            NetworkManager.singleton.StopClient();
        }
        //SceneManager.LoadScene("MainMenu");
    }
}
