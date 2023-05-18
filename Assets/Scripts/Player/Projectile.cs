using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed = 500f;
    [SerializeField] protected float damage = 10;
    [SerializeField] protected GameObject bloodEffect;

    protected Rigidbody2D shot;


    protected virtual void Start()
    {
        shot = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //call in fixed update as physics is involved
        BulletVel();
    }

    void BulletVel()
    {
        //only if the bullet exists
        if (shot != null)
        {
            //add velocity in the direction its facing with bullet speed and time
            shot.velocity = transform.up.normalized * speed * Time.fixedDeltaTime;
        }
    }

    

    public void SetProjectileShot(Rigidbody2D inputRb)
    {
        shot = inputRb;
    }

    public void ResetBulletDamage()
    {
        damage = 10f;
    }


}
