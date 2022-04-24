using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartItem : Item
{
    public override bool Use(Player player)
    {
        Debug.Log("Not a useable item!");
        return false;
    }
}
