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
    float meleeAttackRange = 1.0f;

    [SerializeField]
    float alertRange = 10.0f;


    bool isDead;

    private enum EnemyState
    {
        Idle, // Nothing in sight of enemm
        Alert, // Can see target, not in range of alert
        moveToTarget, // Moving to the target
        moveToStartPosition, // Moving to start posiiton
        attacking // Attack mode
    }

    private EnemyState currentState;

    private Vector3 startingPosition;

    [SerializeField]
    private Rigidbody2D rb;


    private void walkTo(Vector3 destination)
    {
        transform.position = Vector2.MoveTowards(this.transform.position, destination, walkingSpeed * Time.deltaTime);
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
        print(currentState);
        // TODO: Check if in attack range.
        if (target)
        {
            float distanceFromTarget = Vector2.Distance(target.transform.position, transform.position);

            if (distanceFromTarget < meleeAttackRange)
            {
                currentState = EnemyState.attacking;
            }
            else if (distanceFromTarget < alertRange)
            {
                currentState = EnemyState.moveToTarget;
            }
            else
            {
                currentState = EnemyState.Alert;
            }
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
                break;
            case EnemyState.moveToStartPosition:
            case EnemyState.Alert:
                walkTo(startingPosition);
                break;
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") target = other.transform;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player") target = null;
    }



}
