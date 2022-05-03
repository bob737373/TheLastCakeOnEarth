using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Door : GameSceneManager
{
    void OnCollisionEnter2D(Collision2D c)
    {
        // StartCoroutine(loadScene(dest));
        loadScene(dest);
    }
}
