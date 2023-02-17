using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{

    public GameObject spawnPoint;
    public GameObject pin;
    public GameObject throwSpot;
    public float resetTime;
    public float timer;
    public bool pinHit = false;

    // Start is called before the first frame update
    void Start()
    {
        pin = GameObject.FindGameObjectWithTag("ThrowingPin");
        throwSpot = GameObject.FindGameObjectWithTag("ThrowSpot");
    }

    // Update is called once per frame
    void Update()
    {
        if (pinHit)
        { 
            timer += Time.deltaTime;
            if (timer >= resetTime)
            {
                pinHit = false;
                timer = 0f;
                // After timer is up, pin is moved slightly up from it's position.
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.15f, transform.position.z);

                // Pin is rotated towards the player.
                transform.LookAt(throwSpot.transform.position);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == pin)
        {
            pinHit = true;
        }
    }
}
