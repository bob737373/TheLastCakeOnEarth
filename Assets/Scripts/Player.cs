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
        
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Fire1")) {
            Attack();
            print("attack");
        }

        float scroll = Input.GetAxisRaw("Mouse Scrollwheel");
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
        weaponIndex++;
        if (weaponIndex == weapons.Length) weaponIndex = 0;
        selectedWeapon = weapons [weaponIndex];
    }

    void SelectWeapon(int newWeaponIndex) {
        if(newWeaponIndex < weapons.Length && newWeaponIndex >= 0) {
            selectedWeapon = weapons [newWeaponIndex];
            weaponIndex = newWeaponIndex;
        }

    }

    void Attack() {
        if (selectedWeapon) selectedWeapon.Attack(this.enemyLayers);
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}


