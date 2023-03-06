using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Timeline;

public class ThrowingPin : MonoBehaviour
{
    public GameObject spawnPoint;
    public Boolean thrown = false;
    public float resetTime;
    public float timer;
    Rigidbody rb;
    public List<GameObject> hitPinList;
    GameObject hitPin;

    public AudioClip impactSound;
    public GameObject audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GameObject.FindGameObjectWithTag("AudioSource");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        // Pin respawns on the table after 4 seconds from being thrown by the player.
        if (thrown)
        {
            timer += Time.deltaTime;
            if (timer >= resetTime)
            {
                thrown = false;

                timer = 0;
                rb.velocity = Vector3.zero;
                transform.position = spawnPoint.transform.position;
                transform.rotation = spawnPoint.transform.rotation;

                GameManager.manager.countPoints = true;
                // Calculate points if pins were hit. If only one pin is hit the score added is the number of the pin.
                // If multiple pins are hit, then the score is the amount of pins hit.
                //if (hitPinList.Count == 1)
                //{
                //    GameManager.manager.playerScore += hitPin.GetComponent<Pin>().pointAmount;
                //}
                //else if (hitPinList.Count > 1)
                //{
                //    GameManager.manager.playerScore += hitPinList.Count;
                //}

                //// Clear the list for a new throw.
                //hitPinList.Clear();
            }
        }
    }

    public void PinThrown()
    {
        thrown = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.GetComponentInParent<AudioSource>().PlayOneShot(impactSound);

        // Check the pins that are hit.
        if(collision.gameObject.tag == "Pin")
        {

            //hitPin = collision.gameObject;

            //// Check that a pin is not added to the list more than once.
            //if (!hitPinList.Contains(hitPin)) 
            //{
            //    hitPinList.Add(hitPin);
            //}
        }
    }

    // Button on the table resets the position of throwing pin.
    // !! ADD A POINT CALCULATION HERE AS WELL. OR SEPARATE POINT CALCULATION FROM THE RESPAWN !!
    public void ResetPosition()
    {
        thrown = false;

        timer = 0;
        rb.velocity = Vector3.zero;
        transform.position = spawnPoint.transform.position;
        transform.rotation = spawnPoint.transform.rotation;
    }
}
