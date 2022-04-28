using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Oven : MonoBehaviour
{
    [SerializeField]
    private Text flourText;
    [SerializeField]
    private Text milkText;
    [SerializeField]
    private Button bakeButton;

    [SerializeField]
    private Unlockable oven;

    [SerializeField]
    private GameObject ovenMenu;
    
    private Player player;

    private bool menuEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<Player>();
        ovenMenu.SetActive(menuEnabled);
    }

    // Update is called once per frame
    void Update()
    {
        if(oven.getFixed()){
            if(player){  
                if(Input.GetKeyDown("q")){
                    menuEnabled = !menuEnabled;
                    ovenMenu.SetActive(menuEnabled);
                    updateIngredientStatus();
                }
            } else {
                Debug.Log("No player found for menu interaction");
            }
        } 
    }

    private void updateIngredientStatus(){
        if(player.getMilk()){
            milkText.color = Color.green;
        }
        if(player.getFlour()){
            flourText.color = Color.green;
        }
        if(player.getMilk() && player.getFlour()){
            bakeButton.enabled = true;
        } else {
            bakeButton.enabled = false;
        }
    }

    public void bakeCake(){
        SceneManager.LoadScene("EndScene");
    }
}
