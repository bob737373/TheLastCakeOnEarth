using UnityEngine;

public class ConsumableItem : Item
{

    [SerializeField]
    int amtHealthAffected = 0;

    [SerializeField]
    StatusEffect statusEffect = StatusEffect.none;

    public override bool Use(Player player)
    {
        Debug.Log("adding " + amtHealthAffected + " to " + player);
        player.CmdAddHealth(amtHealthAffected);

        if (statusEffect != StatusEffect.none)
        {
            player.applyStatusEffect(new StatusItem((StatusEffect)statusEffect, player));
        }

        return true;
    }

}



