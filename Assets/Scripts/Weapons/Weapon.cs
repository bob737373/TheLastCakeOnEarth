using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public enum WeaponType {melee, ranged};
    
    [SerializeField]
    protected float attackRadius = 0.5f;
    [SerializeField]
    protected int attackDamage;
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected string attackTriggerName = "Attack";
    [SerializeField]
    protected float attackDelay = 0.5f;

    protected bool isReadying;

    public void Attack(LayerMask enemyLayers) {
        if(isReadying) return;
        animator.SetTrigger(attackTriggerName);
        DoAttack(enemyLayers);
        ResetWeapon();
    }

    protected abstract void DoAttack(LayerMask enemyLayers);

    public int GetDamage() {
        return this.attackDamage;
    }

    private void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        animator.ResetTrigger(attackTriggerName);
    }

    public void ResetWeapon() {
        if (isReadying) { 
            return;
        }
        isReadying = true;
        StartCoroutine(ResetAfterTime());
    }

    protected IEnumerator ResetAfterTime() {
        yield return new WaitForSeconds (attackDelay);
        isReadying = false;
    }
}