using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public abstract class Entity : NetworkBehaviour
{

    [SerializeField]
    protected float attackRange = 0.5f;
    [SerializeField]
    protected int meleeAttackDmg = 1;

    [SerializeField]
    protected float attacksPerSecond = 1.0f;
    private float nextDamageEvent;

    [SerializeField]
    protected int maxHealth;

    [SerializeField]
    protected Rigidbody2D rb;

    [SerializeField]
    protected float defaultMoveSpeed = 5f;

    [SerializeField]
    protected float moveSpeed = 5f;

    [SerializeField, SyncVar]
    protected int health;

    // [SyncVar]
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

    }

    //void FixedUpdate() {} 

    protected abstract void Attack();

    public void meleeAttack(Entity target)
    {
        if (Time.time > nextDamageEvent)
        {
            nextDamageEvent = Time.time + attacksPerSecond;
            target.CmdTakeDamage(meleeAttackDmg, StatusEffect.caffeinated);
        }
    }

    public void meleeAttack(HashSet<Entity> targets)
    {
        if (Time.time > nextDamageEvent)
        {
            nextDamageEvent = Time.time + attacksPerSecond;
            foreach (Enemy enemy in targets)
            {   
                float tempAttackRange = attackRange;
                if(enemy.gameObject.GetComponent<CowBoss>()){
                    tempAttackRange += 2;
                }

                if (Vector2.Distance(enemy.transform.position, this.transform.position) <= tempAttackRange)
                {
                    enemy.CmdTakeDamage(meleeAttackDmg, StatusEffect.none);
                }
            }
        }
    }

    [Command(requiresAuthority = false)]
    public virtual void CmdTakeDamage(int damage, StatusEffect status)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
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

    [Command(requiresAuthority = false)]
    public void CmdAddHealth(int healthAmt)
    {
        Debug.Log("server adding " + healthAmt + " to " + this);
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

    public virtual void Die()
    {
        print("dead");
        Destroy(gameObject);
    }

    bool canAttack()
    {
        // TODO: Check if in melee range.
        return true;
    }

}
