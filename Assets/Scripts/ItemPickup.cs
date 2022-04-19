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
        print("ITEM");
        item = new Item(itemType);
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.sprite = item.icon;
    }

    void pickUp()
    {   

        item.player = target;
        if (target.addItem(item))
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
