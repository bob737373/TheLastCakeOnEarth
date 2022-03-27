using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    public int maxHealth = 20;
    public int health = 20;
    public int coins = 0;
    public int icing = 0;

    [SerializeField]
    float defaultMoveSpeed = 5f;
    [SerializeField]
    float moveSpeed = 5f;
    [SerializeField]
    float attackRange = 0.5f;
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    LayerMask enemyLayers;
    [SerializeField]
    Weapon[] weapons;
    [SerializeField]
    Camera cam;

    //booleans for HUD icon display
    bool[] statuses = { false, false, false, false, false };
    /*
    status index list for all use cases of statuses:
    0: caffeinated
    1: coffee crash
    2: minty
    3: spicy
    4: stomach ache
    */

    Vector2 movement;
    Vector2 mousePos;
    Vector3 mousePos3;
    int weaponIndex;
    Weapon selectedWeapon;
    float camZ;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        camZ = cam.transform.position.z;
        if(weapons.Length != 0) {
            selectedWeapon = weapons[0];
        }
        for(int i = 1; i < weapons.Length; i++) {
            weapons[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        cam.transform.rotation = Quaternion.Euler(0,0,0);
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        mousePos3 = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos = mousePos3;

        if (Input.GetButton("Fire1")) {
            Attack();
        }

        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        if (scroll > 0) {
            ChangeWeapon(true);
        } else if (scroll < 0) {
            ChangeWeapon(false);
        }

    }

    void FixedUpdate() 
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        Vector2 lookDir = mousePos - rb.position;
        float ang = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = ang;
    }

    int getHealth(){
        return health;
    }

    int getCoins(){
        return coins;
    }

    int getIcing(){
        return icing;
    }

    void ChangeWeapon(bool increment) {
        if(increment) {
            weaponIndex++;
            if (weaponIndex >= weapons.Length) weaponIndex = 0;
        } else {
            weaponIndex--;
            if (weaponIndex < 0) weaponIndex = weapons.Length-1;
        }
        SelectWeapon (weaponIndex);
        //selectedWeapon = weapons [weaponIndex];
    }

    void SelectWeapon(int newWeaponIndex) {
        if(newWeaponIndex < weapons.Length && newWeaponIndex >= 0) {
            selectedWeapon.gameObject.SetActive(false);
            selectedWeapon = weapons [newWeaponIndex];
            selectedWeapon.gameObject.SetActive(true);
            weaponIndex = newWeaponIndex;
            print(selectedWeapon);
        }

    }

    void Attack() {
        print("attack");
        if (selectedWeapon) selectedWeapon.Attack(this.enemyLayers);
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }


    // METHOD WHEN AN ITEM IS USED
    //-------------------------------------------------------------------------
    void itemUse(string item) {
        if (item == "expiredSnackCakes") {
            health += 3;
            statuses[4] = true;
            timerStart(4);//start stomach timer
        } else if (item == "jellyBeans") {
            health += 5;
        } else if (item == "chocolateBar") {
            health += 8;
        } else if (item == "gourmetChocolates") {
            health = maxHealth;
        } else if (item == "cinnamonCandy") {
            statuses[3] = true;
            timerStart(3);//start spicy timer
        } else if (item == "candyCane") {
            statuses[2] = true;
            timerStart(2);//start minty timer
        } else if (item == "mysteryFlavorTaffy") {
            int randomStatus = Random.Range(0, 5);
            statuses[randomStatus] = true;
            timerStart(randomStatus);//start random status timer
        } else if (item == "cannedEspresso") {
            if (statuses[0]) {
                statuses[0] = false;
                statuses[1] = true;//causes coffee crash
                timerStart(1);//start crash timer
            } else {
                statuses[0] = true;
                timerStart(0);//start caffeine timer
            }
        } else if (item == "cupOfCoffee") {
            health += 5;
            if (statuses[0]) {
                statuses[0] = false;
                statuses[1] = true;//causes coffee crash
                timerStart(1);//start crash timer
            } else {
                statuses[0] = true;
                timerStart(0);//start caffeine timer
            }
        } else if (item == "cinnamonSpicedCoffee") {
            if (statuses[0]) {
                statuses[0] = false;
                statuses[1] = true;//causes coffee crash
                timerStart(1);//start crash timer
            } else {
                statuses[0] = true;
                timerStart(0);//start caffeine timer
            }
            statuses[3] = true;
            timerStart(3);//start spicy timer
        }
        else if (item == "peppermintCoffee") {
            if (statuses[0]) {
                statuses[0] = false;
                statuses[1] = true;//causes coffee crash
                timerStart(1);//start crash timer
            } else {
                statuses[0] = true;
                timerStart(0);//start caffeine timer
            }
            statuses[2] = true;
            timerStart(2);//start minty timer
        }
        else if (item == "gingerAle") {
            statuses[1] = false;
            statuses[4] = false;
            //set attack speed to original value
            maxHealth = 20;
        } else {
            print("wrong item name");
        }
    }
    //-------------------------------------------------------------------------

    //STATUS EFFECTS' STAT EDITS + TIMER
    //-------------------------------------------------------------------------
    void timerStart(int status) {
        if (status == 0) {
            moveSpeed = defaultMoveSpeed * 2;
        } else if (status == 1) {
            moveSpeed = defaultMoveSpeed;
            //TODO: slow attack speed
        } else if (status == 2) {
            //TODO: change enemy movement speed to 0 when hit by player
        } else if (status == 3) {
            //TODO: cause enemy health to decay/burn over short timer after hit by player
        } else if (status == 4) {
            maxHealth = 10;
            if (health > maxHealth) {
                health = maxHealth;
            }
        } else {
            print("status index out of bounds, something screwed up");
        }
        StartCoroutine(statusTimer(status));
    }
    IEnumerator statusTimer(int status) {
        print("start timer");
        yield return new WaitForSeconds(30.0f);
        statuses[status] = false;
        if (status == 0) {
            //uncaffeinated
            moveSpeed = defaultMoveSpeed;
        } else if (status == 1) {
            //recovered from crash
            //set attack speed to original value
        } else if (status == 2) {
            //unminty
            //undo enemy movement speed to 0 when hit by player
        } else if (status == 3) {
            //mild
            //undo enemy health decay/burn over timer after hit by player
        } else if (status == 4) {
            //stomach better
            maxHealth = 20;
        } else {
            print("status index out of bounds");
        }
    }
    //-------------------------------------------------------------------------
}


