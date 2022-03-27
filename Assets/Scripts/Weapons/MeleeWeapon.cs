using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField]
    Transform attackPoint;
    

    protected override void DoAttack(LayerMask enemyLayers)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRadius, enemyLayers);
        foreach(Collider2D enemy in hitEnemies) {
            print("hit " + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage, effect);
        }
    }

}
