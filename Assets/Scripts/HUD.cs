using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private Text coinsText;

    private Text icingText;

    public Image healthBar;

    private Text milkText;

    private Text flourText;

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

        GameObject milkTextGO = GameObject.Find("MilkText");
        milkText = milkTextGO.GetComponent<Text>();

        GameObject flourTextGO = GameObject.Find("FlourText");
        flourText = flourTextGO.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            updateHealth();
            updateCoins();
            updateIcing();
            updateIngredients();
        }
    }


    public void updateHealth()
    {
        healthBar.fillAmount = (float) player.getHealth() / (float) player.getMaxHealth();

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

    public void updateIngredients(){
        if(player.getMilk()){
            milkText.color = Color.green;
        }
        if(player.getFlour()){
            flourText.color = Color.green;
        }
    }

    public void setHealth(float health)
    {
    }
}
