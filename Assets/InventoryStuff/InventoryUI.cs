
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    
    Inventory inventory;

    public GameObject inventoryUI;

    void Start()
    {
        //inventory = Inventory.instance;
        //inventory.onItemChangedCallback += UpdateUI;
    }

    
    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }
}
