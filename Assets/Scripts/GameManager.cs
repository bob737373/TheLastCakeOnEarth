using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    LayerMask playerLayer;
    [SerializeField]
    LayerMask enemyLayer;
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
