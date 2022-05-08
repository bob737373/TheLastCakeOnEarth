using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Unlockable : NetworkBehaviour
{
    
    [SerializeField]
    int numberOfPartsRequired = 20;

    [SerializeField, SyncVar]
    int currentParts = 0;

    [SerializeField, SyncVar]
    public bool unlocked = false;

    [SerializeField]
    Text textBox;
    new BoxCollider2D collider;

    Player player;
    CircleCollider2D playerCollider;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach(var player in players) {
            if(player.name == "Player(local)"){ //TODO - make these tags and name comparison strings constants
                this.player = player.GetComponent<Player>();
                break;
            }
        }
        if(this.player) {
            playerCollider = player.GetComponent<CircleCollider2D>();
        }
        // GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        // player = playerObj.GetComponent<Player>();
    }

    virtual protected void Update()
    {
        if (Input.GetKeyDown("f") && player && collider.IsTouching(playerCollider))
        {
            Debug.Log("input registered");
            while (currentParts < numberOfPartsRequired && player.inventory.TakeItem("Part"))
            {
                CmdAddPart();
                Debug.Log("parts taken");
            }
        }

        if (currentParts >= numberOfPartsRequired)
        {
            textBox.text = $"Fixed!";
            CmdUnlock();
        }
        else
        {
            textBox.text = $"{currentParts}/{numberOfPartsRequired}";

        }
    }

    [Command]
    void CmdAddPart() {
        currentParts++;
    }

    [Command]
    void CmdUnlock() {
        unlocked = true;
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.tag == "Player")
    //     {
    //         player = other.GetComponent<Player>();
    //     }
    // }

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.tag == "Player")
    //     {
    //         player = null;
    //     }
    // }
}
