using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadInitialGame : GameSceneManager
{
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        Debug.Log("Resetting player prefs!!");
        PlayerPrefs.DeleteAll();
        // StartCoroutine(loadScene(dest));
        loadScene(dest);
    }

}
