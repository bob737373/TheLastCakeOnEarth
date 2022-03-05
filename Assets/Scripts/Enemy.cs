using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    int maxHealth;
    int health;
    bool dead;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void attack() {
        
    }

    void receiveDamage(int damage) {
        health -= damage;
        if (health < 0) {
            dead = true;
        }
    }
}
