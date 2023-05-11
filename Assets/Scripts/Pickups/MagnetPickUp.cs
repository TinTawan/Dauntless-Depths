using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPickUp : ItemPickUp
{
    private CoinScript coinScript;
    private SpriteRenderer sr;
    private CircleCollider2D cCol;

    [SerializeField] private float magDist = 2f, magTime = 5f;

    private GameObject[] coins;
    private bool startTimer;
    private float originalTime;

    protected override void Start()
    {
        base.Start();
        sr = GetComponent<SpriteRenderer>();
        cCol = GetComponent<CircleCollider2D>();

        originalTime = magTime;
    }

    private void Update()
    {
        //keeps adding coins to the array in update when new coins are dropped
        coins = GameObject.FindGameObjectsWithTag("Coin");

        if (startTimer)
        {
            //magTime decreases with delta time so the slider 
            magTime -= Time.deltaTime;
            if (magTime <= 0f)
            {
                //end magnetising
                startTimer = false;
                magTime = originalTime;

                //set magval to 0 for all coins
                foreach(GameObject currentCoin in coins)
                {
                    coinScript = currentCoin.GetComponent<CoinScript>();
                    coinScript.SetDistToPlayer(0f);
                }

                //destroy object
                Destroy(gameObject);
            }
            else //while the timer is running
            {
                //get each coin in the scene
                foreach (GameObject currentCoin in coins)
                {
                    coinScript = currentCoin.GetComponent<CoinScript>();

                    //let the coin be magnetised to the player by given distance
                    coinScript.SetDistToPlayer(magDist);

                }
            }

        }

    }


    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.item, transform.position, 1f);

            InstantiateText("Activate Magnet");

            startTimer = true;

            //disable colider and sprite renderer
            sr.enabled = false;
            cCol.enabled = false;
        }
    }



    public float GetMaxMagnetTime()
    {
        return originalTime;
    }
    public float GetMagnetTimer()
    {
        return magTime;
    }

}
