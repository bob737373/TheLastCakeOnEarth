using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestScript : MonoBehaviour
{
   
    public Sprite treasureChestOpen;
    public Sprite treasureChest1;
    public AudioSource source;
    public AudioClip clip;

    public Player player;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        // if (Input.GetKeyDown (KeyCode.Q))
        // {
        //     this.gameObject.GetComponent<SpriteRenderer>().sprite = treasureChestOpen;
        //     source.PlayOneShot(clip);
        //     Debug.Log("yes");
        // }

        if (this.gameObject.GetComponent<SpriteRenderer>().sprite == treasureChest1)
        {      
            if (Input.GetKeyDown (KeyCode.Q) && player)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = treasureChestOpen;
                source.PlayOneShot(clip);
                Debug.Log("yes");
            }       
        }
        
    }


    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            player = other.GetComponent<Player>();
        };
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            player = null;
        };
    }


}
