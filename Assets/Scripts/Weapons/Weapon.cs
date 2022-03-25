using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public enum WeaponType {melee, ranged};
    
    [SerializeField]
    protected float attackRadius = 0.5f;
    [SerializeField]
    protected int attackDamage = 0;

    public abstract void Attack(LayerMask enemyLayers);
}