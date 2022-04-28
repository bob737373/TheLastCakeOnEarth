using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSeed : MonoBehaviour
{
    [SerializeField]
    int seed = -1;

    // Start is called before the first frame update
    void Start()
    {
        int first = Random.Range(1, 4);
        int second = Random.Range(1, 4);
        int third = Random.Range(1, 4);
        int fourth = Random.Range(1, 4);
        if (seed == -1)
        {
            seed = (first * 1000) + (second * 100) + (third * 10) + (fourth);
        }
        // index for first room: (seed / 1000)
        // index for second room: ((seed % 1000) / 100)
        // index for third room: (((seed % 1000) % 100) / 10)
        // index for fourth room: (((seed % 1000) % 100) % 10)

        // creates a simple 4 digit seed with numbers 1-5
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getSeedFor(int room) {
        if (room == 0) {
            return seed; // returns full seed on 0 call
        } else if (room == 1) {
            return (seed / 1000);
        } else if (room == 2) {
            return ((seed % 1000) / 100);
        } else if (room == 3) {
            return (((seed % 1000) % 100) / 10);
        } else if (room == 4) {
            return (((seed % 1000) % 100) % 10);
        } else {
            print("not proper room number on seed call, returning -1");
            return -1;
        }

    }
}
