using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealthUI : MonoBehaviour
{

    Enemy enemy;
    Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        healthBar = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float) enemy.getHealth() / (float) enemy.getMaxHealth();

        if (healthBar.fillAmount < 0.3f)
        {
            healthBar.color = (Color.red);
        }
        else if (healthBar.fillAmount < 0.5f)
        {
            healthBar.color = (Color.yellow);
        }
        else
        {
            healthBar.color = (Color.green);
        }
    }
}
