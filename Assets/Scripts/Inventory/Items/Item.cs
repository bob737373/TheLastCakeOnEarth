using UnityEngine;
using System.Collections.Generic;


public abstract class Item : MonoBehaviour, IPersistentObject
{

    // public enum ItemType {
    //     PartItem,
    //     ChocolateBar,
    //     Coffee
    // }
    // public static readonly Dictionary<ItemType, string> itemTypeDict = new Dictionary<ItemType, string>{
    //     {ItemType.PartItem, "Part"},
    //     {ItemType.ChocolateBar, "Chocolate Bar"},
    //     {ItemType.Coffee, "Coffee"}
    // };
    

    [SerializeField]
    public string itemName;
    // [SerializeField]
    // public ItemType type;

    [SerializeField]
    public Sprite icon;

    public string persistent_unique_id { get; set; }

    protected virtual void Start()
    {
        this.generateID();
        this.shouldSpawn();
    }


    public abstract bool Use(Player player);

    protected void pickUp(Player player)
    {
        if (player.inventory.Add(this))
        {
            setObjectUsed();
            // Destroy item
            //Destroy(gameObject);
            gameObject.SetActive(false);
        };
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            pickUp(other.GetComponent<Player>());
        };
    }

    // Object persistence
    public void generateID()
    {
        this.persistent_unique_id = this.transform.position.sqrMagnitude.ToString();
    }

    public void shouldSpawn()
    {
        string exists = PlayerPrefs.GetString(persistent_unique_id);
        if (exists.Length > 0)
        {
            Destroy(gameObject);
        }
    }
    public void setObjectUsed()
    {
        PlayerPrefs.SetString(persistent_unique_id, "used!");
    }
}