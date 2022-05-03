using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    public Image healthBar;

    [SerializeField]
    private Image milkImg;

    [SerializeField]
    private Image jelloImg;

    [SerializeField]
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        if(!player) {
            player = GetComponentInParent<Player>();
            if(!player) Debug.LogError("HUD doesn't seem to be attached to a player.");
        }

        GameObject imageHealthGO = GameObject.Find("HealthMeter");
        healthBar = imageHealthGO.GetComponent<Image>();

        Color jColor = jelloImg.color;
        jColor.a = .5f;
        jelloImg.color = jColor;

        Color mColor = milkImg.color;
        mColor.a = .5f;
        milkImg.color = mColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            updateHealth();
            updateIngredients();
        }
    }


    public void updateHealth()
    {
        healthBar.fillAmount = (float)player.getHealth() / (float)player.getMaxHealth();

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


    public void updateIngredients()
    {
        if (player.getMilk())
        {
            // milkText.color = Color.green;
            Color mColor = milkImg.color;
            mColor.a = 1.0f;
            milkImg.color = mColor;
        }
        if (player.getFlour())
        {
            // flourText.color = Color.green;
            Color jColor = jelloImg.color;
            jColor.a = 1.0f;
            jelloImg.color = jColor;
        }
    }

    public void setHealth(float health)
    {
    }
}
