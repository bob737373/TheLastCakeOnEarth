using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemPickup : MonoBehaviour
{

    [SerializeField]
    public ItemsEnum itemType;

    Player target;
    Item item;

    void Start()
    {   
        // get sprite from item
        item = new Item(itemType);
        
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.icon;
    }

    void pickUp()
    {   
        if (target.inventory.Add(item))
        {
            // Destroy item
            Destroy(gameObject);
        };
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            target = other.GetComponent<Player>();
            pickUp();
        };
    }
}
