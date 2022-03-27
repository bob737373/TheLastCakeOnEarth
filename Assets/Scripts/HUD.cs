using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text coinsText;
    public Text icingText;
    public Image healthBar;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        coinsText = gameObject.GetComponent<Text>();
        icingText = gameObject.GetComponent<Text>();
        healthBar = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        updateHealth();
        updateCoins();
        updateIcing();
    }

    public void updateHealth(){
        healthBar.fillAmount = (player.health/player.maxHealth);
        if(healthBar.fillAmount < 0.3f)
        {
            healthBar.color = (Color.red);
        }
        else if(healthBar.fillAmount < 0.5f)
        {
            healthBar.color = (Color.yellow);
        }
        else
        {
            healthBar.color = (Color.green);
        }
    }

    public void updateCoins(){
        coinsText.text = player.coins.ToString();
    }

    public void updateIcing(){
        icingText.text = player.icing.ToString();
    }
}
