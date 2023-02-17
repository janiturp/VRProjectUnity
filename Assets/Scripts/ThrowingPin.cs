using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThrowingPin : MonoBehaviour
{
    public GameObject spawnPoint;
    public Boolean thrown = false;
    public float resetTime;
    public float timer;
    public GameObject pin;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // Pin respawns on the table after 4 seconds from being thrown by the player.
        if (thrown)
        {
            timer += Time.deltaTime;
            if (timer >= resetTime)
            {
                thrown = false;
                timer = 0;
                pin.transform.position = spawnPoint.transform.position;
            }
        }
    }

    public void PinThrown()
    {
        thrown = true;
    }
}
