using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashUpgrade : ItemPickUp
{
    [SerializeField] private float dashCooldownReduce = 1f;

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player.AddDashCooldown(-dashCooldownReduce);

            InstantiateText("- Dash Cooldown");

            base.OnTriggerEnter2D(col);
            //Destroy(gameObject);
        }
    }


}
