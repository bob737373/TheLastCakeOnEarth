using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{

    [SerializeField]
    protected float attackRange = 0.5f;
    protected int maxHealth;

    [SerializeField]
    protected Rigidbody2D rb;

    [SerializeField]
    protected float defaultMoveSpeed = 5f;
    
    protected float moveSpeed = 5f;

    [SerializeField]
    protected int health;

    protected Vector2 movement; 
    bool isDead;

    protected abstract void Attack();

    public void TakeDamage(int damage, Player.StatusEffects effect)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    
    public int getHealth()
    {
        return this.health;
    }

    public int getMaxHealth()
    {
        return this.maxHealth;
    }

    public void addHealth(int healthAmt){
        int newHealthAmt = health + healthAmt;

        // Make sure health is not greater then max health
        if (newHealthAmt > maxHealth){
            newHealthAmt = maxHealth;
        }

        this.health = newHealthAmt;
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
