using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField]
    float moveSpeed = 5f;
    [SerializeField]
    float attackRange = 0.5f;
    [SerializeField]
    int attackDamage = 2;
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    LayerMask enemyLayers;
    [SerializeField]
    Weapon[] weapons;

    Vector2 movement;
    int weaponIndex;
    Weapon selectedWeapon;


    void Start()
    {
        if(weapons.Length != 0) {
            selectedWeapon = weapons[0];
        }
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Fire1")) {
            Attack();
            
        }

        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        if (scroll > 0) {
            ChangeWeapon(true);
        } else if (scroll < 0) {
            ChangeWeapon(false);
        }
    }

    void FixedUpdate() 
    {
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        rb.velocity = movement * moveSpeed;
    }

    void ChangeWeapon(bool increment) {
        if(increment) {
            weaponIndex++;
            if (weaponIndex >= weapons.Length) weaponIndex = 0;
        } else {
            weaponIndex--;
            if (weaponIndex < 0) weaponIndex = weapons.Length-1;
        }
        SelectWeapon (weaponIndex);
        //selectedWeapon = weapons [weaponIndex];
    }

    void SelectWeapon(int newWeaponIndex) {
        if(newWeaponIndex < weapons.Length && newWeaponIndex >= 0) {
            selectedWeapon = weapons [newWeaponIndex];
            weaponIndex = newWeaponIndex;
        }

    }

    void Attack() {
        print("attack");
        if (selectedWeapon) selectedWeapon.Attack(this.enemyLayers);
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}


