using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

//booleans for HUD icon display
public enum StatusEffect { none, caffeinated, coffeeCrash, minty, spicy, stomachAche, chocolate }

public class StatusItem
{
    public StatusEffect statusEffect;
    public Entity entity;
    private static System.Timers.Timer aTimer;
    public float time = 5.0f;


    public StatusItem(StatusEffect statusEffect, Entity entity)
    {
        this.entity = entity;
        this.statusEffect = statusEffect;

        applyEffect();
    }


    private float amtMovespeedToAdd = 0.0f;

    private void applyEffect()
    {
        // If already has caffeinated
        if (entity.hasStatusEffect(StatusEffect.caffeinated))
        {   
            this.statusEffect = StatusEffect.coffeeCrash;
        }



        switch (statusEffect)
        {
            case StatusEffect.chocolate:
                entity.CmdAddHealth(8);
                break;
            case StatusEffect.caffeinated:
                amtMovespeedToAdd = 5f;
                entity.addMoveSpeed(amtMovespeedToAdd);
                break;
            case StatusEffect.coffeeCrash:
                // Apply coffeeCrash stats
                entity.setMoveSpeed(entity.getDefaultMoveSpeed());
                break;
            case StatusEffect.minty:
                break;
            case StatusEffect.spicy:
                break;
            case StatusEffect.stomachAche:
                break;
        }

        Debug.Log(statusEffect);
    }

    public void removeEffect()
    {
        // undo effect
        entity.addMoveSpeed(-amtMovespeedToAdd);

        /// remove it from that entities current
        entity.removeStatusEffect(this.statusEffect);
    }





}