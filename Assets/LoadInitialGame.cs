using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum Destination
{
    BAKERY_AREA,
    CITY_EAST,
    CITY_WEST,
    CITY_NORTH,
    CITY_SOUTH,
    BAKERY_INTERIOR,
    EGG_FACTORY_INTERIOR,
    MILK_FACTORY_INTERIOR,
    SUGAR_FACTORY_INTERIOR,
    FLOUR_FACTORY_INTERIOR,
    SCENE_LOAD,

}

/* 
DEST COORDINATES
    BAKERY_INTERIOR: 5, -3




*/
public class LoadInitialGame : MonoBehaviour
{

    private GameObject player;
    private GameObject inventory;
    private GameObject hud;
    private GameObject menu;
    private GameObject eventSystem;


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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        inventory = GameObject.FindGameObjectsWithTag("Inventory")[0];
        hud = GameObject.FindGameObjectsWithTag("HUD")[0];
        menu = GameObject.FindGameObjectsWithTag("Menu")[0];
        eventSystem = GameObject.FindGameObjectsWithTag("EventSystem")[0];

        StartCoroutine(loadScene(dest));
    }

    IEnumerator loadScene(Destination dest)
    {
        string name = getScene(dest);

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

    IEnumerator unloadScene(string name)
    {
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
            SceneManager.MoveGameObjectToScene(inventory, sceneToLoad);
            SceneManager.MoveGameObjectToScene(hud, sceneToLoad);
            SceneManager.MoveGameObjectToScene(menu, sceneToLoad);
            SceneManager.MoveGameObjectToScene(eventSystem, sceneToLoad);
            player.transform.position = new Vector2(spawnx, spawny);
            SceneManager.SetActiveScene(sceneToLoad);
            SceneManager.MoveGameObjectToScene(player, sceneToLoad);
        }
    }

    void OnFinishedLoadingAllScene(string name)
    {
        Debug.Log("Done Loading Scene");
        enableScene(name);
        Debug.Log("Scene Activated!");
        StartCoroutine(unloadScene(getScene(src)));
    }

    private string getScene(Destination d)
    {
        switch (d)
        {
            case Destination.SCENE_LOAD:
                return "LoadScene";
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
