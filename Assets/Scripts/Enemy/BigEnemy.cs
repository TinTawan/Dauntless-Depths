using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : Enemy
{

    [Header("Big Enemy Only")]
    [SerializeField] private GameObject slimePrefab;
    [SerializeField] private GameObject lifePrefab;
    [SerializeField] private float spawnDelay = 2f;
    [SerializeField] private int randomDropRange = 5;

    private float slimeTimer;

    protected override void Start()
    {
        base.Start();
        offset = new Vector3(0, 2f);
    }

    protected override void Update()
    {
        TurnToPlayer();
        base.EnemyMove();


        slimeTimer += Time.deltaTime;
        if(slimeTimer >= spawnDelay)
        {
            SpawnSlime();
            slimeTimer = 0f;
        }


        if(health <= 0)
        {
            DropLifeItem();

        }

    }

    protected override void TurnToPlayer()
    {
        if (transform.position.x > pc.transform.position.x)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }
    }

    protected override IEnumerator DamageColour()
    {
        sr.color = Color.magenta;
        yield return new WaitForSeconds(0.1f);
        sr.color = originalColour;
    }

    void SpawnSlime()
    {
        //intantiate slime
        Instantiate(slimePrefab, transform.position, Quaternion.identity);


    }

    void DropLifeItem()
    {
        int rand = Random.Range(0, randomDropRange);
        if (rand == 1)
        {
            Instantiate(lifePrefab, transform.position, Quaternion.identity);
        }
    }

}
