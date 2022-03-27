using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{

    [SerializeField]
    protected float attackRange = 0.5f;
    [SerializeField]
    protected int maxHealth;
    [SerializeField]
    protected Rigidbody2D rb;
    [SerializeField]
    protected float defaultMoveSpeed = 5f;
    
    protected float moveSpeed = 5f;
    protected int health;
    protected Vector2 movement; 
    bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    //void Update() {}

    //void FixedUpdate() {} 

    protected abstract void Attack();

    public void TakeDamage(int damage, Player.StatusEffects effect)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        print("died");
    }

    bool canAttack()
    {
        // TODO: Check if in melee range.
        return true;
    }

}
