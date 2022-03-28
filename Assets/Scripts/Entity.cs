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

    List<StatusItem> currentStatusEffects = new List<StatusItem>();


    // Start is called before the first frame update
    public virtual void Start()

    {



        moveSpeed = this.defaultMoveSpeed;
        maxHealth = this.health;
    }



    // Update is called once per frame
    public virtual void Update()
    {
        foreach (StatusItem se in currentStatusEffects)
        {
            print("ZXC" + se.statusEffect);
        }
    }

    //void FixedUpdate() {} 

    protected abstract void Attack();

    public void TakeDamage(int damage, StatusEffect status)
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

    public float getDefaultMoveSpeed()
    {
        return this.defaultMoveSpeed;
    }

    public int getMaxHealth()
    {
        return this.maxHealth;
    }

    public void addHealth(int healthAmt)
    {
        int newHealthAmt = health + healthAmt;

        // Make sure health is not greater then max health
        if (newHealthAmt > maxHealth)
        {
            newHealthAmt = maxHealth;
        }

        this.health = newHealthAmt;
    }


    public void addMoveSpeed(float moveSpeed)
    {

        if ((moveSpeed + this.moveSpeed) < defaultMoveSpeed)
        {
            this.moveSpeed = defaultMoveSpeed;
        }
        this.moveSpeed += moveSpeed;
    }
    public void setMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }


    // Remove status effect from this entity
    public void removeStatusEffect(StatusEffect statusEffect)
    {
        // Search effects
        StatusItem itemToRemove = currentStatusEffects.Find(e => e.statusEffect == statusEffect);
        currentStatusEffects.Remove(itemToRemove);
    }

    public void applyStatusEffect(StatusItem statusItem)
    {
        currentStatusEffects.Add(statusItem);

        StartCoroutine(removeEffectTimer(statusItem));
    }

    IEnumerator removeEffectTimer(StatusItem statusItem)
    {
        yield return new WaitForSeconds(statusItem.time);
        statusItem.removeEffect();
    }

    public bool hasStatusEffect(StatusEffect statusEffect)
    {
        // Search effects
        StatusItem hasItem = currentStatusEffects.Find(e => e.statusEffect == statusEffect);
        if (hasItem != null)
        {
            return true;
        }
        else
        {
            return false;
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
