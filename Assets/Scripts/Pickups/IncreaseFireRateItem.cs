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
            if(FindObjectOfType<PlayerShoot>().GetFireRate() > 0.01)
            {
                pShoot.AddFireRate(fireRateIncreaseAmount);
                InstantiateText("+ Fire Rate");
            }
            else
            {
                InstantiateText("Max Fire Rate");
            }

            base.OnTriggerEnter2D(col);
        }
    }

}

