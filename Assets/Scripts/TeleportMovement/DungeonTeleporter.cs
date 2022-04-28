using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonTeleporter : MonoBehaviour
{
    [SerializeField]
    private Transform destination;
    [SerializeField]
    private Transform destination2;
    [SerializeField]
    private Transform destination3;
    [SerializeField]
    int floor;
    [SerializeField]
    int roomNumber;
    [SerializeField]
    bool exit;

    public bool isExit() {
        return exit;
    }

    public int GetFloor() {
        return floor;
    }

    public Transform GetDestination(int roomNumber)
    {
        if (roomNumber == 1)
        {
            return destination;
        }
        else if (roomNumber == 2)
        {
            return destination2;
        }
        else if (roomNumber == 3)
        {
            return destination3;
        }
        else
        {
            return null;
        }
    }
}
