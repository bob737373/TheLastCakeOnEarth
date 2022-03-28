using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingItems : MonoBehaviour
{

    [SerializeField]
    public Player player;



    // METHOD WHEN AN ITEM IS USED
    //-------------------------------------------------------------------------
    public void itemUse(int statusEffect)
    {
        player.applyStatusEffect(new StatusItem((StatusEffect)statusEffect, this.player));
    }

}
