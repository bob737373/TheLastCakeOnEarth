using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    int maxHealth;

    [SerializeField]
    float walkingSpeed = 1f;

    int health;
    private Transform target = null;
    private Vector2 movement;

    [SerializeField]
    float meleeAttackRange = 15.0f;

    bool isDead;

    private enum EnemyState
    {
        Idle,
        moveToTarget,
        moveToStartPosition
    }

    private EnemyState currentState;

    private Vector3 startingPosition;

    [SerializeField]
    private Rigidbody2D rb;

    
    private void walkTo(Vector3 destination)
    {
       transform.position = Vector2.Lerp(transform.position, destination, Time.deltaTime);
    //    transform.position = Vector2.MoveTowards(this.transform.position, position, step);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.Idle;
        startingPosition = this.transform.position;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
        // TODO: Check if in attack range.
        if (target)
        {
            currentState = EnemyState.moveToTarget;
        }
        else if (startingPosition != this.transform.position)
        {
            currentState = EnemyState.moveToStartPosition;
        }
        else
        {
            currentState = EnemyState.Idle;
        }


        handleState();
    }

    void FixedUpdate()
    {

    }

    private void handleState()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                {
                    print("idling");
                    break;
                }
            case EnemyState.moveToStartPosition:
                {
                    print("Moving back to start position");
                    walkTo(startingPosition);
                    break;
                }
            case EnemyState.moveToTarget:
                {
                    walkTo(target.transform.position);
                    break;
                }
            default: break;
        }
    }

    protected virtual void attack()
    {

    }

    public void TakeDamage(int damage)
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") target = other.transform;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player") target = null;
    }



}
