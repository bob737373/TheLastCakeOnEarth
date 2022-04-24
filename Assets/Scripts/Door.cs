using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Destination{
    BAKERY_AREA = 0,
    CITY_EAST = 1,
    CITY_WEST = 2,
    CITY_NORTH = 3,
    CITY_SOUTH = 4,
    BAKERY_INTERIOR = 5,
    EGG_FACTORY_INTERIOR = 6,
    MILK_FACTORY_INTERIOR = 7,
    SUGAR_FACTORY_INTERIOR = 8,
    FLOUR_FACTORY_INTERIOR = 9
}

/* 
DEST COORDINATES
    BAKERY_INTERIOR: 5, -3




*/

public class Door : MonoBehaviour
{
    
    [SerializeField]
    Destination dest;

    [SerializeField]
    Destination src;

    [SerializeField]
    float spawnx;

    [SerializeField]
    float spawny;

    private AsyncOperation sceneAsync;
    private AsyncOperation unloadAsync;

    private GameObject player;
    private GameObject inventory;
    private GameObject hud;
    private GameObject menu;

    void Start() {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        inventory = GameObject.FindGameObjectsWithTag("Inventory")[0];
        hud = GameObject.FindGameObjectsWithTag("HUD")[0];
        menu = GameObject.FindGameObjectsWithTag("Menu")[0];
    }

    void OnCollisionEnter2D(Collision2D c){
        StartCoroutine(loadScene(getScene(dest)));
    }

    IEnumerator loadScene(string name) {
        AsyncOperation scene = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        scene.allowSceneActivation = true;
        sceneAsync = scene;

        while (scene.progress < 1f)
        {
            Debug.Log("Loading scene " + " [][] Progress: " + scene.progress);
            yield return null;
        } 
        OnFinishedLoadingAllScene(name);
    }

    IEnumerator unloadScene(string name) {
        AsyncOperation unload = SceneManager.UnloadSceneAsync(name);
        unload.allowSceneActivation = true;

        while (unload.progress < 1f)
        {
            Debug.Log("Unloading scene " + " [][] Progress: " + unload.progress);
            yield return null;
        } 
        Debug.Log("Unloaded");
    }

    void enableScene(string name)
    {
        Scene sceneToLoad = SceneManager.GetSceneByName(name);
        if (sceneToLoad.IsValid())
        {
            Debug.Log("Scene is Valid");
            SceneManager.MoveGameObjectToScene(player, sceneToLoad);
            SceneManager.MoveGameObjectToScene(inventory, sceneToLoad);
            SceneManager.MoveGameObjectToScene(hud, sceneToLoad);
            SceneManager.MoveGameObjectToScene(menu, sceneToLoad);
            player.transform.position = new Vector2(spawnx, spawny);
            SceneManager.SetActiveScene(sceneToLoad);
        }
    }

    void OnFinishedLoadingAllScene(string name)
    {
        Debug.Log("Done Loading Scene");
        enableScene(name);
        Debug.Log("Scene Activated!");
        StartCoroutine(unloadScene(getScene(src)));
    }

    private string getScene(Destination d){
        switch(d){
            case Destination.BAKERY_AREA:
                return "SampleScene";
            case Destination.CITY_EAST:
                return "CityEastScene";
            case Destination.CITY_WEST:
                return "CityWestScene";
            case Destination.CITY_NORTH:
                return "CityNorthScene";
            case Destination.CITY_SOUTH:
                return "CitySouthScene";
            case Destination.BAKERY_INTERIOR:
                return "BakeryScene";
            case Destination.EGG_FACTORY_INTERIOR:
                return "EggFactoryScene";
            case Destination.MILK_FACTORY_INTERIOR:
                return "MilkFactoryScene";
            case Destination.SUGAR_FACTORY_INTERIOR:
                return "SugarFactoryScene";
            case Destination.FLOUR_FACTORY_INTERIOR:
                return "FlourFactoryScene";
            default: 
                return "TestScene";        
        }
    }
}