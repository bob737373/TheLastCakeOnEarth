using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    [SerializeField]
    float alertRange = 10.0f;

    enum EnemyState
    {
        idle, // Nothing in sight of enemm
        alert, // Can see target, not in range of alert
        moveToTarget, // Moving to the target
        moveToStartPosition, // Moving to start posiiton
        attacking // Attack mode
    }
    EnemyState currentState;

    Vector3 startingPosition;
    Transform target = null;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        startingPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //print(currentState);
        // TODO: Check if in attack range.
        if (target)
        {
            float distanceFromTarget = Vector2.Distance(target.transform.position, transform.position);

            if (distanceFromTarget < attackRange)
            {
                currentState = EnemyState.attacking;
            }
            else if (distanceFromTarget < alertRange)
            {
                currentState = EnemyState.moveToTarget;
            }
            else
            {
                currentState = EnemyState.alert;
            }
        }
        else if (startingPosition != this.transform.position)
        {
            currentState = EnemyState.moveToStartPosition;
        }
        else
        {
            currentState = EnemyState.idle;
        }


        HandleState();
    }

    protected override void Attack() {

    }

    void WalkTo(Vector3 destination)
    {
        transform.position = Vector2.MoveTowards(this.transform.position, destination, moveSpeed * Time.deltaTime);
    }

    private void HandleState()
    {
        switch (currentState)
        {
            case EnemyState.idle:
                break;
            case EnemyState.moveToStartPosition:
            case EnemyState.alert:
                WalkTo(startingPosition);
                break;
            case EnemyState.moveToTarget:
                {
                    WalkTo(target.transform.position);
                    break;
                }
            default: break;
        }
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
