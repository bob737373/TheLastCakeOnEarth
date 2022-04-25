using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity, IPersistentObject
{

    [SerializeField]
    float alertRange = 10.0f;

    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip enemyOof;
    [SerializeField]
    AudioClip enemyDie;

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

    public string persistent_unique_id { get; set; }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        currentState = EnemyState.idle;
        startingPosition = this.transform.position;

        shouldSpawn();
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
        setObjectUsed();
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

    public void generateID()
    {
        this.persistent_unique_id = this.transform.position.sqrMagnitude.ToString();
    }

    public void shouldSpawn()
    {
        generateID();

        string exists = PlayerPrefs.GetString(persistent_unique_id);
        if (exists.Length > 0)
        {
            Destroy(gameObject);
        }
    }

    public void setObjectUsed()
    {
        PlayerPrefs.SetString(persistent_unique_id, "dead!");
    }
}
