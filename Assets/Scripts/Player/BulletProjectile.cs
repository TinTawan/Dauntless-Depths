using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : Projectile
{
    [SerializeField] GameObject hitEffect;

    protected override void Start()
    {
        base.Start();
        FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.shoot, transform.position, 1f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //if the collision is with an enemy
        if (col.CompareTag("Enemy"))
        {
            //damage the enemy with the given bullet damage
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            enemy.Damage((int)damage);

        }


        if (shot != null)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);

            //destroy the bullet after it collides with anything
            Destroy(shot.gameObject);
        }
        
    }

    public void IncreaseDamage(float inputDamage)
    {
        damage += inputDamage;
    }

}
