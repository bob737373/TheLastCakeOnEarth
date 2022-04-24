using UnityEngine;


public abstract class Item : MonoBehaviour
{
    [SerializeField]
    public string itemName;

    [SerializeField]
    public Sprite icon;

    public abstract bool Use(Player player);

    protected void pickUp(Player player)
    {
        if (player.inventory.Add(this))
        {
            // Destroy item
            Destroy(gameObject);
        };
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            pickUp(other.GetComponent<Player>());
        };
    }




}