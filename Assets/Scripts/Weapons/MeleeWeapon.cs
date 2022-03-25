using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField]
    float attackRadius = 0.5f;
    [SerializeField]
    int attackDamage = 5;
    [SerializeField]
    Transform attackPoint;

    public override void Attack(LayerMask enemyLayers)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRadius, enemyLayers);
        foreach(Collider2D enemy in hitEnemies) {
            print("hit " + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

}