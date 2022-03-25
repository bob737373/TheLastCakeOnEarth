using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    int maxHealth;
    int health;
    bool dead;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void attack() {
        
    }

    public void TakeDamage(int damage) {
        print("ouch");
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        print("died");
    }

    bool canAttack() {
        return true;
    }
}
