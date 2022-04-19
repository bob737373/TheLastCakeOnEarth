using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField]
    Player player;

    [SerializeField]
    GameObject inventoryUI;
    public int space = 9;
    public List<Item> items = new List<Item>();

    private List<InventorySlot> slots = new List<InventorySlot>();

    public bool open = false;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player not defined!!!");
        }

        inventoryUI.SetActive(true);

        // Cache all of our slots
        for (int i = 0; i <= space - 1; i++)
        {
            slots.Add(GameObject.Find($"InventorySlot ({i})").GetComponent<InventorySlot>());
        }

        inventoryUI.SetActive(open);
    }


    public bool Add(Item item)
    {
        if (items.Count >= space)
        {
            Debug.Log("not enough room in inventory");
            inventoryUI.SetActive(open);
            return false;
        }

        items.Add(item);

        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }

    void UpdateUI()
    {
        if (!inventoryUI.activeSelf) return;
        for (int i = 0; i <= items.Count - 1; i++)
        {
            slots[i].buttonAction = onItemUse;
            slots[i].AddItem(items[i]);
        }

        // Clear rest of the slots
        for (int i = items.Count; i <= space - 1; i++)
        {
            slots[i].ClearSlot();
        }
    }

    void onItemUse(Item item)
    {
        item.Consume(player);
        items.Remove(item);
    }


    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            open = !inventoryUI.activeSelf;
            inventoryUI.SetActive(open);
        }
        
        UpdateUI();
    }

}
