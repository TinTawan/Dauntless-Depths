using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] private float lifeTime = 3f;
    private float deathTimer;

    protected override void Start()
    {
        base.Start();

    }


    protected override void Update()
    {
        deathTimer += Time.deltaTime;
        if (deathTimer >= lifeTime)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.01f);
        }
        else
        {
            sr.flipX = transform.position.x > pc.transform.position.x;
            //moves toward player if timer hasnt reached max
            EnemyMove();
        }



    }
}
