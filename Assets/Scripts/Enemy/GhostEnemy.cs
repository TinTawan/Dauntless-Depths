using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemy : Enemy
{
    protected override void Update()
    {
        TurnToPlayer();
        base.EnemyMove();

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




}
