
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    Item item;

    

    [SerializeField]
    Image itemImage;

    Button button;

    [SerializeField]
    GameObject buttonGO;

    void Start()
    {
        button = buttonGO.GetComponent<Button>();
        button.onClick.AddListener(() => item.Consume());
        buttonGO.SetActive(false);
    }

    public void AddItem(Item newItem, int slotNumber)
    {
        item = newItem;
        newItem.slotNumber = slotNumber;
        buttonGO.SetActive(true);
        itemImage.sprite = item.icon;
    }

    public void ClearSlot()
    {
        item = null;
        buttonGO.SetActive(false);
    }
}
