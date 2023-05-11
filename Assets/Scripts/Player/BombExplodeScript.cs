using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplodeScript : MonoBehaviour
{
    private Enemy enemy;

    [SerializeField] private int bombDamage = 100;


    private void OnParticleCollision(GameObject other)
    {        
        //only for the 1st colHit do this
        if (other.CompareTag("Enemy"))
        {
            enemy = other.GetComponent<Enemy>();

            enemy.Damage(bombDamage);
        }


    }

}
