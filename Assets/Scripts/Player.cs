using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class Player : Entity
{

    [SerializeField]
    GameObject CameraMountPoint;
    [SerializeField]
    Camera cameraPrefab;
    [SerializeField]
    GameObject HUDPrefab;

    [SerializeField]
    WeaponContainer[] weapons;

    [SerializeField]
    LayerMask enemyLayers;
    [SerializeField]
    Animator animator;

    [SerializeField]
    public Inventory inventory;

    enum Direction { down = -1, right = 2, up = 1, left = -2, none = 0 };
    public enum StatusEffects { caffeinated, coffeeCrash, minty, spicy, stomachAche }

    Camera cam;
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
    [SerializeField]
    AudioClip swish;
    [SerializeField]
    AudioClip death;

    [SerializeField]
    public bool milk;

    [SerializeField]
    public bool flour;

    HashSet<Entity> enemyList = new HashSet<Entity>();

    protected bool walking = false;

    public override void Start()
    {
        base.Start();
        if (!inventory)
        {
            inventory = FindObjectOfType<Inventory>();
            if(!inventory) Debug.LogError("Inventory is not defined in GUI!");
        }

        if(isLocalPlayer) {
            GameObject hud = Instantiate(HUDPrefab);
            //hud.transform.parent = this.transform;
            hud.transform.SetParent(this.transform, false); //unity said to do this, don't really get why but whatever
            cam = Instantiate(cameraPrefab);
            Transform cameraTransform = Camera.main.gameObject.transform;  //Find main camera which is part of the scene instead of the prefab
            cameraTransform.parent = CameraMountPoint.transform;  //Make the camera a child of the mount point
            cameraTransform.position = CameraMountPoint.transform.position;  //Set position/rotation same as the mount point
            cameraTransform.rotation = CameraMountPoint.transform.rotation;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            camZ = cam.transform.position.z;
        }

        animator.SetInteger("direction", (int)Direction.none);

    }

    public override void Update()
    {
        base.Update();
        if(!isLocalPlayer) { return; }
        cam.transform.rotation = Quaternion.Euler(0, 0, 0);
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        // Make sure inventory isnt open
        if (Input.GetButton("Fire1") && !inventory.open)
        {
            Attack();
            audioSource.PlayOneShot(swish, .2f);
        }
    

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
        }

    }

    void FixedUpdate()
    {
        if(!isLocalPlayer) { return; } 
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    [Command(requiresAuthority = false)]
    public override void CmdTakeDamage(int damage, StatusEffect status)
    {
        base.CmdTakeDamage(damage, status);
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

    public bool getMilk()
    {
        return milk;
    }

    public bool getFlour()
    {
        return flour;
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

        animator.SetBool("isAttacking", true);
        this.meleeAttack(this.enemyList);
    }

    void ResetAttack()
    {
        animator.SetBool("isAttacking", false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.transform.GetComponent<Enemy>();
        if (enemy != null)
        {
            this.enemyList.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Enemy enemy = other.transform.GetComponent<Enemy>();
        if (enemy != null && this.enemyList.Contains(enemy) == true)
        {
            this.enemyList.Remove(enemy);
        }
    }

    public override void Die()
    {
        this.enemyList = new HashSet<Entity>();
        audioSource.PlayOneShot(death, 2f);
        StartCoroutine(deathEn());
    }

    IEnumerator deathEn() {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("DeathScreen");
    }

}

[System.Serializable]
public struct WeaponContainer
{

    public string label;// { get; private set; }
    public Weapon weapon;// { get; private set; }

}