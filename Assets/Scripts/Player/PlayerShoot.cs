using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Rigidbody2D bulletPrefab, bombPrefab;
    [SerializeField] private Pointer mousePointer;
    [SerializeField] private GameObject shootEffect, casingEffect;
    [SerializeField] private Transform endOfGunPoint;

    private Projectile projectile;

    //button to shoot
    [SerializeField] private KeyCode shootKey, bombKey;

    //delay to shoot a bullet
    [SerializeField] private float shootDelay = 1f, bombDelay = 3f;
    private float shootTimer, bombTimer;



    private void Update()
    {
        //can only shoot if in playing state
        if (Manager.canvas.currentState == CanvasManager.GameState.Playing)
        {
            shootTimer += Time.deltaTime;
            bombTimer += Time.deltaTime;

            //shoot when holding the shoot button
            if (Input.GetKey(shootKey))
            {
                //acts as a fire rate
                if (shootTimer >= shootDelay)
                {
                    Instantiate(shootEffect, endOfGunPoint.position, transform.rotation);
                    ShootBullet();

                    //reset timer after every shot
                    shootTimer = 0f;
                }
            }
            if (Input.GetKeyDown(bombKey) && Manager.unlockedBomb)
            {
                if (bombTimer >= bombDelay)
                {
                    Instantiate(shootEffect, endOfGunPoint.position, transform.rotation);
                    ShootBomb();

                    bombTimer = 0f;
                }
            }
        }

        
    }

    void ShootBullet()
    {
        Instantiate(casingEffect, transform.position, transform.rotation);

        //intantiate bullet
        Rigidbody2D shot = Instantiate(bulletPrefab, endOfGunPoint.transform.position, transform.rotation);

        projectile = GameObject.FindGameObjectWithTag("Projectile").GetComponent<Projectile>();

        //pass information to the bullet script
        projectile.SetProjectileShot(shot);

    }

    void ShootBomb()
    {
        //intantiate bomb
        Rigidbody2D bomb = Instantiate(bombPrefab, transform.position, transform.rotation);

        projectile = GameObject.FindGameObjectWithTag("Projectile").GetComponent<Projectile>();

        //pass information to the bullet script
        projectile.SetProjectileShot(bomb);

    }

    public float GetBombTimer()
    {
        return bombTimer;
    }
    public float GetBombShootDelay()
    {
        return bombDelay;
    }

    public float GetFireRate()
    {
        return shootDelay;
    }
    public void AddFireRate(float inputFireRate)
    {
        shootDelay -= inputFireRate;
    }
}
