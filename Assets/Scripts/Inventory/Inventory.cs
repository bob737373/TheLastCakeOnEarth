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

    private List<InventorySlot> slots = new List<InventorySlot>();

    public bool open = false;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player not defined!!!");
        }

        inventoryUI.SetActive(true);

        // Cache/setup all of our slots
        for (int i = 0; i <= space - 1; i++)
        {
            InventorySlot slot = GameObject.Find($"InventorySlot ({i})").GetComponent<InventorySlot>();
            slot.player = this.player;
            slots.Add(slot);
        }

        inventoryUI.SetActive(open);
    }


    private int getActiveSlotsCount()
    {
        int count = 0;
        foreach (InventorySlot slot in slots)
        {
            if (slot.isActive())
            {
                count++;
            }
        }

        return count;
    }

    

    private InventorySlot getNextUnactiveSlot()
    {
        foreach (InventorySlot slot in slots)
        {
            if (!slot.isActive())
            {
                return slot;
            }
        }

        return null;
    }

    public bool Add(Item item)
    {
        // Add to existing slot if exists and not full. 
        foreach (InventorySlot slot in slots)
        {
            if (slot.containsItemType(item))
            {
                if (slot.addItem(item))
                {
                    return true;
                };
            }
        }

        // Cant add a new slot if slot inventory is full as well.
        if (getActiveSlotsCount() >= space)
        {
            Debug.Log("not enough room in inventory");
            inventoryUI.SetActive(open);
            return false;
        }

        InventorySlot unactive = getNextUnactiveSlot();
        unactive.addItem(item);

        return true;
    }

    // Take an item from the users inventory
    public bool TakeItem(string itemName){
        foreach(InventorySlot slot in slots){
            foreach(Item item in slot.items){
                if(item.itemName == itemName){
                    slot.removeItem(item);
                    return true;
                }
            }
        }
        return false;
    }


    public List<Item> getItems(){
        List<Item> items = new List<Item>();
        foreach(InventorySlot slot in slots){
            items.AddRange(slot.items);
        }
        
        return items;
    }

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            open = !inventoryUI.activeSelf;
            inventoryUI.SetActive(open);
        }
    }

}
