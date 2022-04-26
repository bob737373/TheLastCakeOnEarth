using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* 
    COORDS
    In an ideal world, this would be in some kind of data structure for convenience.
    Reality is rarely so kind.
    Instead, I've plugged all of these coordinates into the appropriate doors across all of the scenes
    
    Default Spawn: 33, 5
    Central -> South:28, 1
    Central -> East: 350, 452
    Central -> Bakery: 5, -5
    East -> Central: 72, 18
    East -> MilkFactory: 0, 1
    South -> Central: 19, -11
    South -> FlourFactory: 0, 1
    Bakery -> Central: 33, 5
    FlourFactory -> South: 64, -11
    MilkFactory -> East: 371, 487
*/

public enum ScenesEnum
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

public class GameSceneManager : MonoBehaviour
{
    private List<GameObject> gameObjects = new List<GameObject>();


    [SerializeField]
    protected ScenesEnum dest;

    [SerializeField]
    ScenesEnum src;

    [SerializeField]
    float spawnx;

    [SerializeField]
    float spawny;

    private AsyncOperation sceneAsync;
    private AsyncOperation unloadAsync;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        gameObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("TransitionElements"));
        gameObjects.Add(GameObject.FindGameObjectWithTag("Player"));
    }

    protected IEnumerator loadScene(ScenesEnum dest)
    {
        string name = getScene(dest);
        AsyncOperation scene = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        scene.allowSceneActivation = true;
        sceneAsync = scene;

        while (scene.progress < 1f)
        {
            yield return null;
        }
        OnFinishedLoadingAllScene(name);
    }

    protected IEnumerator unloadScene(ScenesEnum dest)
    {
        string name = getScene(dest);

        AsyncOperation unload = SceneManager.UnloadSceneAsync(name);
        unload.allowSceneActivation = true;

        while (unload.progress < 1f)
        {
            Debug.Log("Unloading scene " + " [][] Progress: " + unload.progress);
            yield return null;
        }
        Debug.Log("Unloaded");
    }

    protected void enableScene(string name)
    {
        Scene sceneToLoad = SceneManager.GetSceneByName(name);
        if (sceneToLoad.IsValid())
        {
            Debug.Log("Scene is Valid");
            foreach (GameObject go in gameObjects)
            {
                SceneManager.MoveGameObjectToScene(go, sceneToLoad);
            }

            GameObject player = gameObjects[gameObjects.Count - 1];
            player.transform.position = new Vector2(spawnx, spawny);
            SceneManager.SetActiveScene(sceneToLoad);
        }
    }

    protected void OnFinishedLoadingAllScene(string name)
    {
        Debug.Log("Done Loading Scene");
        enableScene(name);
        Debug.Log("Scene Activated!");
        StartCoroutine(unloadScene(src));
    }

    protected string getScene(ScenesEnum d)
    {
        switch (d)
        {
            case ScenesEnum.SCENE_LOAD:
                return "LoadScene";
            case ScenesEnum.BAKERY_AREA:
                return "CentralScene";
            case ScenesEnum.CITY_EAST:
                return "CityEastScene";
            case ScenesEnum.CITY_WEST:
                return "CityWestScene";
            case ScenesEnum.CITY_NORTH:
                return "CityNorthScene";
            case ScenesEnum.CITY_SOUTH:
                return "CitySouthScene";
            case ScenesEnum.BAKERY_INTERIOR:
                return "BakeryScene";
            case ScenesEnum.EGG_FACTORY_INTERIOR:
                return "EggFactoryScene";
            case ScenesEnum.MILK_FACTORY_INTERIOR:
                return "MilkFactoryScene";
            case ScenesEnum.SUGAR_FACTORY_INTERIOR:
                return "SugarFactoryScene";
            case ScenesEnum.FLOUR_FACTORY_INTERIOR:
                return "FlourFactoryScene";
            default:
                return "TestScene";
        }
    }
}
