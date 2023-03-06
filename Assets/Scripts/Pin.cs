using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{

    public GameObject spawnPoint;
    public GameObject throwingPin;
    public GameObject throwSpot;
    public float resetTime;
    public float timer;
    public bool pinHit = false;
    public float rotationCheck;
    public float upAngle;
    public bool debugFallen = false;
    public int pointAmount;

    public AudioClip impactSound;
    public GameObject audioSource;

    // Start is called before the first frame update
    void Start()
    {
        throwingPin = GameObject.FindGameObjectWithTag("ThrowingPin");
        throwSpot = GameObject.FindGameObjectWithTag("ThrowSpot");
        audioSource = GameObject.FindGameObjectWithTag("AudioSource");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        upAngle = Vector3.Angle(transform.up, Vector3.up);

        if (upAngle > rotationCheck)
        {

            if (pinHit && !GameManager.manager.hitPinList.Contains(this.gameObject))
            {
                GameManager.manager.hitPinList.Add(this.gameObject);
                pinHit = false;
            }

            timer += Time.deltaTime;
            if (timer >= resetTime)
            {
                timer = 0f;
                // After timer is up, pin is moved up slightly from it's position.
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.15f, transform.position.z);

                // Pin is rotated towards the player.
                transform.LookAt(throwSpot.transform.position);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject == throwingPin)
        {
            pinHit = true;
        }
    }
}
