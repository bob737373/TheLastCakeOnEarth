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

public class Door : MonoBehaviour
{
    
    [SerializeField]
    Destination dest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D c){
        SceneManager.LoadScene(getScene(dest));
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
