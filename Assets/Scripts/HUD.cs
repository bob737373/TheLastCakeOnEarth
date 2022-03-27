using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private Text coinsText;

    private Text icingText;

    public Image healthBar;

    [SerializeField]
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        GameObject coinsTextGO = GameObject.Find("CoinCountText");
        coinsText = coinsTextGO.GetComponent<Text>();

        GameObject icingTextGO = GameObject.Find("IcingCountText");
        icingText = icingTextGO.GetComponent<Text>();

        GameObject imageHealthGO = GameObject.Find("HealthMeter");
        healthBar = imageHealthGO.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            updateHealth();
            updateCoins();
            updateIcing();

        }
    }


    public void updateHealth()
    {
        healthBar.fillAmount = (player.health / player.maxHealth);
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

    public void updateCoins()
    {
        this.coinsText.text = player.getCoins().ToString();
    }

    public void updateIcing()
    {
        icingText.text = player.getIcing().ToString();
    }

    public void setHealth(float health)
    {
    }
}
