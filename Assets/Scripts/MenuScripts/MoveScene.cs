using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScene : MenuTriggers
{
    [SerializeField] private string sceneName;
    [SerializeField] private Projectile bullet;

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        ChangeScene(sceneName);

        bullet.ResetBulletDamage();
    }
}
