
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class InventorySlot : MonoBehaviour
{


    // public int numItems { get; private set; } = 0;
    // public Item.ItemType itemType { get; private set; }


    // public bool AddItem(Item item) {
    //     if(numItems <= maxItemCount || item.type != itemType) { return false; }
    //     numItems++;
    //     buttonUI.SetActive(true);
    //     itemImageUI.sprite = item.icon;
    //     countTxt.text = items.Count.ToString();
    //     return true;
    // }

    // public bool RemoveItem(Item item) {
    //     if(numItems <= 0 || item.type != itemType) { return false; }
    //     numItems--;
    //     if (items.Count == 0)
    //     {
    //         clearSlot();
    //     }
    //     return true;
    // }




    public List<Item> items = new List<Item>();

    [SerializeField]
    Image itemImageUI;

    [SerializeField]
    public GameObject buttonUI;
    private Button button;
    public Action<Item> buttonAction;

    [SerializeField]
    Text countTxt;

    public Player player;

    [SerializeField]
    int maxItemCount = 20;

    void Start()
    {
        button = buttonUI.GetComponent<Button>();
        button.onClick.AddListener(useItem);
        buttonUI.SetActive(false);
    }

    void Update()
    {
        countTxt.text = items.Count.ToString();
    }

    public void useItem()
    {
        if (items[0].Use(player))
        {
            removeItem(items[0]);
        };
    }

    public bool containsItemType(Item itemToCheck)
    {
        if (items.Count != 0)
        {
            Debug.Log("here: " + items[0].itemName + " check: " + itemToCheck.itemName);
            return items[0].itemName == itemToCheck.itemName;
        }
        return false;
    }

    public bool addItem(Item newItem)
    {
        if (items.Count >= maxItemCount)
        {
            Debug.Log("Max items in this slot!");
            return false;
        }


        items.Add(newItem);
        newItem.transform.parent = this.transform;
        // DontDestroyOnLoad(newItem);
        buttonUI.SetActive(true);
        itemImageUI.sprite = newItem.icon;
        countTxt.text = items.Count.ToString();
        return true;
    }

    public void removeItem(Item item)
    {
        items.Remove(item);
        Destroy(item.gameObject);
        countTxt.text = items.Count.ToString();
        if (items.Count == 0)
        {
            clearSlot();
        }
    }

    public bool isActive()
    {
        return this.items.Count >= 1;
    }
    public void clearSlot()
    {
        buttonUI.SetActive(false);
    }
}
