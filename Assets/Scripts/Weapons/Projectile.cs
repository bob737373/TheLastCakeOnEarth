using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    int aoeDamageMultiplier;

    [SerializeField]
    bool isDespawnable;

    [SerializeField]
    float despawnTime;
    [SerializeField]
    float speedMultiplier;

    RangedWeapon weapon;
    Player.StatusEffects effect;
    float radius;
    int damage;
    LayerMask enemyLayer;

    //new used to override deprecated variable Component.rigidbody
    private new Rigidbody2D rigidbody;

    private string targetTag;

    private void Start() {
        rigidbody = GetComponent<Rigidbody2D> ();
        weapon = GetComponentInParent<RangedWeapon> ();
        effect = weapon.GetEffect();
        radius = weapon.GetAttackRadius();
        damage = weapon.GetDamage();
        transform.parent = null;
        gameObject.name = "Icing Ball";
    }

    public void Shoot(Vector2 force, LayerMask enemyLayer) {
        this.enemyLayer = enemyLayer;
        // rigidbody.isKinematic = false;
        GetComponent<Rigidbody2D>().AddForce(force * speedMultiplier, ForceMode2D.Impulse);
        //transform.SetParent(null);
        if(isDespawnable) StartCoroutine(DespawnAfterTime()); //don't let fly infinitely
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Enemy e = collision.collider.GetComponent<Enemy>();
        e.TakeDamage(damage, effect);
        Collider2D[] aoeHits = Physics2D.OverlapCircleAll(collision.transform.position, radius, enemyLayer);
        foreach(Collider2D enemy in aoeHits) {
            print("hit " + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(damage * aoeDamageMultiplier, effect);
        }
        print("hit " + collision.collider.name);
        Destroy(gameObject);

    }

    private IEnumerator DespawnAfterTime() {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
