using UnityEngine;

public class ConsumableItem : Item
{

    [SerializeField]
    int amtHealthAffected = 0;

    [SerializeField]
    StatusEffect statusEffect = StatusEffect.none;

    public override bool Use(Player player)
    {
        player.addHealth(amtHealthAffected);

        if (statusEffect != StatusEffect.none)
        {
            player.applyStatusEffect(new StatusItem((StatusEffect)statusEffect, player));
        }

        return true;
    }

}



