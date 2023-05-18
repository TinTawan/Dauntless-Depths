using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : Projectile
{

    [SerializeField] GameObject bombParticle;

    protected override void Start()
    {
        base.Start();
        FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.bombShoot, transform.position, 1f);
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        StartCoroutine(FindObjectOfType<CamFollow>().BigCamShake());

        FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.explode, transform.position, 1f);

        Instantiate(bombParticle, transform.position, Quaternion.identity);
        Destroy(shot.gameObject, 0.1f);


        if (col.CompareTag("Enemy"))
        {
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
        }
    }

}
