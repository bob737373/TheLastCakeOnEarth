using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [SerializeField]
    float projectileSpeed = 0.5f;
    [SerializeField]
    Transform projectileSpawnPoint;
    [SerializeField]
    Projectile projectilePrefab; //change to gameobject?

    string targetTag;
    bool isReloading;

    protected override void DoAttack(LayerMask enemyLayers) {
        Instantiate(projectilePrefab, projectileSpawnPoint).Shoot(projectileSpawnPoint.up * projectileSpeed);
    }

}
