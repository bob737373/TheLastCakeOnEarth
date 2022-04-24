
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class InventorySlot : MonoBehaviour
{

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
        buttonUI.SetActive(true);
        itemImageUI.sprite = newItem.icon;
        countTxt.text = items.Count.ToString();
        return true;
    }

    public void removeItem(Item item)
    {
        items.Remove(item);
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
