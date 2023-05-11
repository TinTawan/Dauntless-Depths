using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseFireRateItem : ItemPickUp
{
    [SerializeField] private float fireRateIncreaseAmount = 0.02f;


    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            pShoot.AddFireRate(fireRateIncreaseAmount);
            InstantiateText("+ Fire Rate");

            base.OnTriggerEnter2D(col);
            //Destroy(gameObject);
        }
    }

}

