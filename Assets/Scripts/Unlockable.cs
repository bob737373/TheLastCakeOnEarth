using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unlockable : MonoBehaviour
{

    [SerializeField]
    int numberOfPartsRequired;

    [SerializeField]
    int currentParts = 0;

    [SerializeField]
    bool ovenfixed = false;

    [SerializeField]
    Text textBox;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown("f") && player)
        {
            while (currentParts < numberOfPartsRequired && player.inventory.TakeItem("Part"))
            {
                currentParts++;
            }
        }

        if (currentParts == numberOfPartsRequired)
        {
            textBox.text = $"Fixed!";
            ovenfixed = true;
        }
        else
        {
            textBox.text = $"{currentParts}/{numberOfPartsRequired}";

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player = other.GetComponent<Player>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player = null;
        }
    }

    public bool getFixed(){
        return ovenfixed;
    }
}
