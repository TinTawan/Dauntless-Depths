using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpgrade : ItemPickUp
{
    [SerializeField] private float speedIncrease = 3f;

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player.AddSpeed(speedIncrease, 0.01f);
            InstantiateText("+ Speed");

            base.OnTriggerEnter2D(col);
            //Destroy(gameObject);
        }
    }


}
