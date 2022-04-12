using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    [SerializeField]
    float alertRange = 10.0f;

    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip enemyOof;
    [SerializeField]
    AudioClip enemyDie;

    private Vector3 oldPosition;

    enum MoveDirection
    {
        up,
        down,
        left,
        right
    }

    private MoveDirection moveDir;


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
    public override void Start()
    {
        base.Start();
        currentState = EnemyState.idle;
        startingPosition = this.transform.position;
    }

    void LateUpdate()
    {
        oldPosition = this.transform.position;
    }

    // Update is called once per frame
    public override void Update()
    {
        //test code, nuke
        if (Input.GetKeyDown("]"))
        {
            this.TakeDamage(10000000, StatusEffect.caffeinated);
            print("kaboom");
        }

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

        MoveDirection oldMov = moveDir;
        double xDelta = Mathf.Abs(transform.position.x - oldPosition.x);
        double yDelta = Mathf.Abs(transform.position.y - oldPosition.y);
        // Animations
        if (transform.position.x > oldPosition.x)
        {
            moveDir = MoveDirection.right;
        }
        if (transform.position.x < oldPosition.x)
        {
            moveDir = MoveDirection.left;
        }

        // If moving more in y direction then x
        if (yDelta > xDelta)
        {
            if (transform.position.y > oldPosition.y)
            {
                moveDir = MoveDirection.up;
            }
            if (transform.position.y < oldPosition.y)
            {
                moveDir = MoveDirection.down;
            }
        }

        if (oldMov != moveDir){
            print(moveDir);
        }

    }

    protected override void Attack()
    {

    }

    public override void TakeDamage(int damage, StatusEffect status)
    {
        base.TakeDamage(damage, status);
        audioSource.PlayOneShot(enemyOof, 1f);
    }

    public override void Die()
    {
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        audioSource.PlayOneShot(enemyDie, 1f);
        moveSpeed = 0;
        yield return new WaitForSeconds(0.5f);
        base.Die();
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
            case EnemyState.attacking:
                var x = target.GetComponent<Entity>();
                this.meleeAttack(x);
                // this.WalkTo(x);
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
