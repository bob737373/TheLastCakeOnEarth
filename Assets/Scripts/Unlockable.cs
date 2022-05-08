using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    string homeScene;
    protected new BoxCollider2D collider;

    protected Player player;
    protected CircleCollider2D playerCollider;

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
        DontDestroyOnLoad(this.gameObject);
        // GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        // player = playerObj.GetComponent<Player>();
    }

    virtual protected void Update()
    {
        if(SceneManager.GetActiveScene().name == homeScene && !this.gameObject.activeSelf) {
            var unlockables = FindObjectsOfType<Unlockable>();
            foreach(var u in unlockables) {
                if(!u.Equals(this)) {
                    Destroy(u.gameObject);
                } 
            }
            this.gameObject.SetActive(true);
        } else if(SceneManager.GetActiveScene().name != homeScene && this.gameObject.activeSelf) {
            Debug.Log("hiding oven");
            this.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown("f") && player && collider.IsTouching(playerCollider))
        {
            Debug.Log("input registered");
            while (currentParts < numberOfPartsRequired && player.inventory.TakeItem("Part"))
            {
                CmdAddPart();
                Debug.Log("parts taken");
            }
        }

        if (currentParts >= numberOfPartsRequired && !unlocked)
        {
            textBox.text = $"Fixed!";
            CmdUnlock();
        }
        else if(!unlocked)
        {
            textBox.text = $"{currentParts}/{numberOfPartsRequired}";

        }
    }

    [Command(requiresAuthority = false)]
    void CmdAddPart() {
        currentParts++;
        Debug.Log("server increment parts to " + currentParts);
    }

    [Command(requiresAuthority = false)]
    void CmdUnlock() {
        unlocked = true;
        Debug.Log("server unlock oven");
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
