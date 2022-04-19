
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventorySlot : MonoBehaviour
{
    Item item;

    [SerializeField]
    Image itemImageUI;

    [SerializeField]
    GameObject buttonUI;
    private Button button;
    public Action<Item> buttonAction;

    void Start()
    {
        button = buttonUI.GetComponent<Button>();
        button.onClick.AddListener(() => buttonAction(item));
        buttonUI.SetActive(false);
    }

    public void AddItem(Item newItem)
    {   
        item = newItem;
        buttonUI.SetActive(true);
        itemImageUI.sprite = newItem.icon;
    }

    public void ClearSlot()
    {
        item = null;
        buttonUI.SetActive(false);
    }
}
