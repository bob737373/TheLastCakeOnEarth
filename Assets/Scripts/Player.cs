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

    Vector2 movement;

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
    }

    void FixedUpdate() 
    {
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        rb.velocity = movement * moveSpeed;
    }

    void Attack() {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies) {
            print("hit " + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}


