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
    [SerializeField]
    float shootDelay = 0.5f;

    string targetTag;
    bool isReloading;

    public override void Attack(LayerMask enemyLayers)
    {
        Fire(projectileSpeed);
        print("pew");
    }


    public void Reload() {
        if (isReloading) { 
            return;
        }
        isReloading = true;
        StartCoroutine(ReloadAfterTime());
    }

    private IEnumerator ReloadAfterTime() {
        yield return new WaitForSeconds (shootDelay);
        isReloading = false;
    }

    void Fire(float firePower) {
        if (isReloading){
            return;
        }
        Instantiate(projectilePrefab, projectileSpawnPoint).Shoot(projectileSpawnPoint.up * projectileSpeed);
        Reload();
    }

}
