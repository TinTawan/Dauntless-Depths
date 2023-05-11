using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnItems;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private BoxCollider2D doorCol;
    [SerializeField] private Vector2 newColOffset;

    [SerializeField] private float timeTilOpen = 3f;

    float timer;
    bool startTimer;

    private void Update()
    {
        if (startTimer)
        {
            timer += Time.deltaTime;
            if(timer >= timeTilOpen)
            {
                OpenDoor();
            }
        }
    }

    void SpawnItem()
    {
        //choose random item to spawn at each of the spawn points
        foreach(Transform t in spawnPoints)
        {
            int randChoice = Random.Range(0, spawnItems.Length);

            Instantiate(spawnItems[randChoice], t.position, Quaternion.identity);
        }

        GetComponent<BoxCollider2D>().enabled = false;
    }

    void OpenDoor()
    {
        //allows player to open door
        doorCol.offset = newColOffset;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SpawnItem();
            startTimer = true;
        }

    }

}
