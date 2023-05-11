using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    [SerializeField] protected GameObject[] spawnObject;
    [SerializeField] private float spawnTime;

    private BoxCollider2D[] boxCols;

    private Bounds colBounds;

    protected Vector3 randPoint;

    private bool canSpawn;

    private void Start()
    {
        boxCols = GetComponents<BoxCollider2D>();
    }


    //when the player enters the area
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            canSpawn = true;

            spawnTime = Random.Range(2, spawnTime);

            if (canSpawn)
            {
                InvokeRepeating(nameof(SpawnInMultipleBoxes), 2f, spawnTime);

            }

            canSpawn = false;

        }
    }
    
    protected void SpawnInMultipleBoxes()
    {

        int randNumInBoxCols = Random.Range(0, boxCols.Length);
        Vector3 choosePointInBoxes = new(Random.Range(boxCols[randNumInBoxCols].bounds.min.x, boxCols[randNumInBoxCols].bounds.max.x),
            Random.Range(boxCols[randNumInBoxCols].bounds.min.y, boxCols[randNumInBoxCols].bounds.max.y), 0f);

        int randNumInSpawnObject = Random.Range(0, spawnObject.Length);
        Instantiate(spawnObject[randNumInSpawnObject], choosePointInBoxes, Quaternion.identity);

    }

    public void SetSpawn(bool inputBool)
    {
        canSpawn = inputBool;
    }

}
