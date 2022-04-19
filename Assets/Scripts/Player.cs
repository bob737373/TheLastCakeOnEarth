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
    GameObject inventoryUI;

    public Item[] items = new Item[9];

    //booleans for HUD icon display
    enum Direction { down, right, up, left, none };
    public enum StatusEffects { caffeinated, coffeeCrash, minty, spicy, stomachAche }
    bool[] statuses = { false, false, false, false, false };


    /*
    status index list for all use cases of statuses:
    0: caffeinated
    1: coffee crash
    2: minty
    3: spicy
    4: stomach ache
    */
    Vector2 mousePos;
    Vector3 mousePos3;
    Vector2 mouseDir;
    Direction direction;
    Direction attackDir;
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
        animator.SetInteger("Direction", (int)Direction.none);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        camZ = cam.transform.position.z;
        if (weapons.Length != 0)
        {
            selectedWeapon = weapons[0].weapon;
        }
        for (int i = 1; i < weapons.Length; i++)
        {
            weapons[i].weapon.gameObject.SetActive(false);
        }

    }

    public override void Update()
    {
        base.Update();


        cam.transform.rotation = Quaternion.Euler(0, 0, 0);
        movement.x = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("SpeedX", movement.x);
        animator.SetBool("MovingHorizontally", movement.x != 0);
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("SpeedY", movement.y);
        if (movement.x > 0)
        {
            direction = Direction.right;
            animator.SetBool("Moving", true);
        }
        else if (movement.x < 0)
        {
            direction = Direction.left;
            animator.SetBool("Moving", true);
        }
        else if (movement.y < 0)
        {
            direction = Direction.down;
            animator.SetBool("Moving", true);
        }
        else if (movement.y > 0)
        {
            direction = Direction.up;
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
        animator.SetInteger("Direction", (int)direction);

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


        if (Input.GetButton("Fire1"))
        {
            if (!inventoryUI.activeSelf)
            {
                Attack();
            }
        }

        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        if (scroll > 0)
        {
            ChangeWeapon(true);
        }
        else if (scroll < 0)
        {
            ChangeWeapon(false);
        }

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        //rb.rotation = ang;
    }


    // Call when picking up item
    public bool addItem(Item itemToAdd)
    {   
        // Gets first null index in item array
        int getFirstNullIndex()
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)
                {
                    return i;
                }
            }

            return -1;
        }

        int index = getFirstNullIndex();

        // Attach item to player
        if (index == -1)
        {
            // TODO: Display inventoiry full on UI
            print("Inventory full!");
            return false;
        }
        items[index] = itemToAdd;

        InventorySlot slot = GameObject.Find($"InventorySlot ({index})").GetComponent<InventorySlot>();
        slot.AddItem(itemToAdd, index);

        if (slot != null)
        {
            return true;
        }
        print("Inventory slot returned null !?!?!?!?!?!?");
        return false;


    }



    public void consumeItem(int index)
    {
        print(index);
        print(items.Length);
        this.items[index] = null;
        GameObject.Find($"InventorySlot ({index})").GetComponent<InventorySlot>().ClearSlot();
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

    void ChangeWeapon(bool increment)
    {
        if (increment)
        {
            weaponIndex++;
            if (weaponIndex >= weapons.Length) weaponIndex = 0;
        }
        else
        {
            weaponIndex--;
            if (weaponIndex < 0) weaponIndex = weapons.Length - 1;
        }
        SelectWeapon(weaponIndex);
        //selectedWeapon = weapons [weaponIndex];
    }

    void SelectWeapon(int newWeaponIndex)
    {
        if (newWeaponIndex < weapons.Length && newWeaponIndex >= 0)
        {
            selectedWeapon.gameObject.SetActive(false);
            selectedWeapon = weapons[newWeaponIndex].weapon;
            selectedWeapon.gameObject.SetActive(true);
            weaponIndex = newWeaponIndex;
        }
    }

    override protected void Attack()
    {
        mouseDir = mousePos - rb.position;
        float ang = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
        float rot = ang - 90f;
        if (ang >= 135 || ang <= -135)
        {
            attackDir = Direction.left;
        }
        else if (ang >= 45)
        {
            attackDir = Direction.up;
        }
        else if (ang >= -45)
        {
            attackDir = Direction.right;
        }
        else if (ang >= -135)
        {
            attackDir = Direction.down;
        }
        animator.SetInteger("AttackDir", ((int)attackDir));
        animator.SetTrigger("Attack");
        selectedWeapon.transform.rotation = Quaternion.Euler(0, 0, rot);//ang;
        if (selectedWeapon) selectedWeapon.Attack(this.enemyLayers);
        StartCoroutine(ResetAttackTrigger());
    }

    IEnumerator ResetAttackTrigger()
    {
        yield return new WaitForSeconds(0.1f); //WaitForEndOfFrame();
        animator.ResetTrigger("Attack");
        animator.SetInteger("AttackDir", (int)Direction.none);
    }

}

[System.Serializable]
public struct WeaponContainer
{

    public string label;// { get; private set; }
    public Weapon weapon;// { get; private set; }

}