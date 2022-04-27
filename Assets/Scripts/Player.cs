using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    [SerializeField]
    WeaponContainer[] weapons;

    [SerializeField]
    LayerMask enemyLayers;
    [SerializeField]
    Camera cam;
    [SerializeField]
    Animator animator;

    [SerializeField]
    public Inventory inventory;

    enum Direction { down = -1, right = 2, up = 1, left = -2, none = 0 };
    public enum StatusEffects { caffeinated, coffeeCrash, minty, spicy, stomachAche }

    Vector2 mousePos;
    Vector3 mousePos3;
    Direction direction;
    int weaponIndex;
    Weapon selectedWeapon;
    float camZ;

    [SerializeField]
    private int coins = 0;

    [SerializeField]
    private int icing = 0;

    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip oof;
    [SerializeField]
    AudioClip stepHard;


    protected bool walking = false;

    public override void Start()
    {
        base.Start();
        if (!inventory)
        {
            Debug.LogError("Inventory is not defined in GUI!");
        }

        animator.SetInteger("Direction", (int)Direction.none);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        camZ = cam.transform.position.z;

    }

    public override void Update()
    {
        base.Update();
        cam.transform.rotation = Quaternion.Euler(0, 0, 0);
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x > 0)
        {
            direction = Direction.right;
        }
        else if (movement.x < 0)
        {
            direction = Direction.left;
        }
        else if (movement.y < 0)
        {
            direction = Direction.down;
        }
        else if (movement.y > 0)
        {
            direction = Direction.up;
        }
        else
        {
            direction = Direction.none;
        }

        animator.SetInteger("direction", (int)direction);

        movement.Normalize();
        mousePos3 = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos = mousePos3;

        if (movement.x != 0 || movement.y != 0)
        {
            if (!walking)
            {
                StartCoroutine(walk());
            }
        }


        // Make sure inventory isnt open
        if (Input.GetButton("Fire1") && !inventory.open)
        {
            Attack();
        }


    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        //rb.rotation = ang;
    }

    public override void TakeDamage(int damage, StatusEffect status)
    {
        base.TakeDamage(damage, status);
        audioSource.PlayOneShot(oof, 1f);
    }

    IEnumerator walk()
    {
        walking = true;
        audioSource.PlayOneShot(stepHard, 0.1f);
        yield return new WaitForSeconds(0.25f);
        walking = false;
    }

    public int getCoins()
    {
        return coins;
    }

    public int getIcing()
    {
        return icing;
    }



    override protected void Attack()
    {
        animator.SetBool("isAttacking", true);
    }

    void ResetAttack()
    {
        animator.SetBool("isAttacking", false);
    }


}

[System.Serializable]
public struct WeaponContainer
{

    public string label;// { get; private set; }
    public Weapon weapon;// { get; private set; }

}