using UnityEngine;

public enum ItemsEnum
{
    Coffee,
    ChocolateBar,
    ExpiredSnackCakes,
    JellyBeans
};

public class Item
{
    public string itemName;
    public int slotNumber;
    public ItemsEnum itemType;
    public Sprite icon;

    int amtHealthAffected = 0;

    StatusEffect statusEffect = StatusEffect.none;

    public Item(ItemsEnum itemType)
    {
        string path = "ItemIcons";
        switch (itemType)
        {
            case ItemsEnum.ChocolateBar:
                itemName = "Chocolate Bar";
                icon = Resources.Load($"{path}/chocolateBar", typeof(Sprite)) as Sprite;
                amtHealthAffected = 8;
                break;
            case ItemsEnum.Coffee:
                itemName = "Coffee";
                icon = Resources.Load($"{path}/coffee", typeof(Sprite)) as Sprite;
                amtHealthAffected = 5;
                statusEffect = StatusEffect.caffeinated;
                break;
        }
    }

    public void Consume(Player player)
    {
        player.addHealth(amtHealthAffected);

        if (statusEffect != StatusEffect.none)
        {
            player.applyStatusEffect(new StatusItem((StatusEffect)statusEffect, player));
        }
    }


}



