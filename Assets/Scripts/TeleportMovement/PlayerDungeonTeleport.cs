using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDungeonTeleport : MonoBehaviour
{
    private GameObject currentTeleporter;
    private int floor;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentTeleporter != null)
            {
                //checks if the teleporter is set as an exit teleporter, which if it is, will go back to the previous room instead of the next room
                if (currentTeleporter.GetComponent<DungeonTeleporter>().isExit() != true) {
                    floor = currentTeleporter.GetComponent<DungeonTeleporter>().GetFloor();
                    transform.position = currentTeleporter.GetComponent<DungeonTeleporter>().GetDestination(GetComponent<SimpleSeed>().getSeedFor(floor + 1)).position;
                } else {
                    floor = currentTeleporter.GetComponent<DungeonTeleporter>().GetFloor();
                    if(floor - 1 != 0){
                        transform.position = currentTeleporter.GetComponent<DungeonTeleporter>().GetDestination(GetComponent<SimpleSeed>().getSeedFor(floor - 1)).position;
                    } else {
                        transform.position = currentTeleporter.GetComponent<DungeonTeleporter>().GetDestination(1).position;
                    }  
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
            }
        }
    }
}