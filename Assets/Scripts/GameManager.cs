using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public int playerScore;

    public GameObject scoreBoardNr;

    public GameObject pinSpawnFormation;
    public GameObject pinSpawnPoint;
    public GameObject pinSpawnPrefab;

    public List<GameObject> hitPinList;
    GameObject hitPin;
    public bool countPoints = false;

    public GameObject[] winEffectPositions;
    public GameObject winEffect;
    public AudioClip winSound;
    public GameObject audioSource;

    private void Awake()
    {
        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
        scoreBoardNr = GameObject.FindGameObjectWithTag("ScoreNr");

        pinSpawnPoint = GameObject.FindGameObjectWithTag("PinSpawnPoint");
        //pinSpawnFormation = GameObject.FindGameObjectWithTag("PinSpawnFormation");

        winEffectPositions = GameObject.FindGameObjectsWithTag("WinEffectPosition");

        audioSource = GameObject.FindGameObjectWithTag("AudioSource");
    }

    // Update is called once per frame
    void Update()
    {

        scoreBoardNr.GetComponent<TMP_Text>().text = playerScore.ToString();

        if (pinSpawnFormation == null)
        {
            pinSpawnFormation = GameObject.FindGameObjectWithTag("PinSpawnFormation");
        }

        if (countPoints)
        {
            // Calculate points if pins were hit. If only one pin is hit the score added is the number of the pin.
            // If multiple pins are hit, then the score is the amount of pins hit.
            if (hitPinList.Count == 1)
            {
                playerScore += hitPinList[0].GetComponent<Pin>().pointAmount;
            }
            else if (hitPinList.Count > 1)
            {
                playerScore += hitPinList.Count;
            }

            // Clear the list for a new throw.
            hitPinList.Clear();
            countPoints = false;
        }

        if (playerScore == 50)
        {
            audioSource.GetComponent<AudioSource>().PlayOneShot(winSound);

            for (int i = 0; i < winEffectPositions.Length; i++)
            {
                Instantiate(winEffect, winEffectPositions[i].transform.position, winEffectPositions[i].transform.rotation);
            }
        }
    }

    public void ResetGame()
    {
        playerScore = 0;

        Destroy(pinSpawnFormation);

        Instantiate(pinSpawnPrefab, pinSpawnPoint.transform.position, pinSpawnPoint.transform.rotation);

        pinSpawnFormation = null;
    }
}
