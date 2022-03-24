using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [SerializeField]
    float projectileSpeed = 0.5f;

    public override void Attack(LayerMask enemyLayers)
    {
        throw new System.NotImplementedException();
    }

}
