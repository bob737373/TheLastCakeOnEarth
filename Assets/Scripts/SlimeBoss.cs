using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss : Enemy
{
    public override void Die()
    {
        base.Die();

        GameObject.FindWithTag("Player").GetComponent<Player>().flour = true;
    }
}
