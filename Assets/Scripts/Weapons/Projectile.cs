using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    int damageMultiplier;

    [SerializeField]
    bool isDespawnable;

    [SerializeField]
    float despawnTime;
    [SerializeField]
    float speedMultiplier;

    private RangedWeapon weapon;

    //new used to override deprecated variable Component.rigidbody
    private new Rigidbody2D rigidbody;

    private string targetTag;


    private void Start() {
        rigidbody = GetComponent<Rigidbody2D> ();
        weapon = GetComponentInParent<RangedWeapon> ();
        gameObject.name = "Icing Ball";
    }

    public void Shoot(Vector2 force) {
        // rigidbody.isKinematic = false;
        GetComponent<Rigidbody2D>().AddForce(force * speedMultiplier, ForceMode2D.Impulse);
        //transform.SetParent(null);
        if(isDespawnable) StartCoroutine(DespawnAfterTime()); //don't let fly infinitely
    }

    void OnCollisionEnter2D(Collision2D collision) {

        collision.collider.GetComponent<Enemy>().TakeDamage(weapon.GetDamage() * damageMultiplier);
        print("hit " + collision.collider.name);
        Destroy(gameObject);

    }

    private IEnumerator DespawnAfterTime() {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
