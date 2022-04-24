using UnityEngine;

public enum ItemsEnum
{
    Coffee,
    ChocolateBar,
    ExpiredSnackCakes,
    JellyBeans
};

public class ConsumableItem : Item
{

    [SerializeField]
    int amtHealthAffected = 0;

    [SerializeField]
    StatusEffect statusEffect = StatusEffect.none;

    public override void Use(Player player)
    {
        player.addHealth(amtHealthAffected);

        if (statusEffect != StatusEffect.none)
        {
            player.applyStatusEffect(new StatusItem((StatusEffect)statusEffect, player));
        }
    }

    void pickUp(Player player)
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



