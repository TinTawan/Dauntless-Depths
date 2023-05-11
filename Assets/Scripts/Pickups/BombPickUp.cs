using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPickUp : ItemPickUp
{
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Manager.unlockedBomb = true;
            InstantiateText("+ Bomb");
        }

        base.OnTriggerEnter2D(col);
        //Destroy(gameObject);
    }

}
