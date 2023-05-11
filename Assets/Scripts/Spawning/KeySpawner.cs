using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawner : Spawning
{
    [SerializeField] private int numToSpawn, spawnKeyTime;

    private float keyTimer, endTimer;
    private bool startTimer;


    private void Update()
    {
        if (startTimer)
        {
            endTimer += Time.deltaTime;
            keyTimer += Time.deltaTime;

            for (int i = 0; i < numToSpawn; i++)
            {
                
                if (keyTimer >= spawnKeyTime)
                {
                    SpawnInMultipleBoxes();
                    keyTimer = 0f;
                }

                if (Manager.keys >= numToSpawn)
                {
                    startTimer = false;
                }
            }

            if(endTimer >= spawnKeyTime * numToSpawn + 1f)
            {
                startTimer = false;
            }
            
        }

        
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            startTimer = true;


        }
    }

}
