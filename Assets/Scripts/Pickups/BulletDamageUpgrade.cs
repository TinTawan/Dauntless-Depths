using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamageUpgrade : ItemPickUp
{

    [SerializeField] private BulletProjectile bulletScript;
    [SerializeField] private float addDamage = 5f;



    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && bulletScript != null)
        {
            bulletScript.IncreaseDamage(addDamage);
            InstantiateText("+ Damage");

            base.OnTriggerEnter2D(col);
            //Destroy(gameObject);
        }
    }

}
