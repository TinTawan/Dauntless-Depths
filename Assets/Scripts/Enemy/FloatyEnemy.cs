using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatyEnemy : Enemy
{

    Animator anim;

    protected override void Start()
    {
        base.Start();

        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Vector2 moveVector = new Vector2(hVel, vVel);
        Vector2 moveVel = moveVector.normalized;
        rb.velocity = moveVel * moveSpeed;
    }

    protected override void EnemyMove()
    {
        if (pc != null)
        {
            PlayerRange();

            switch (currentState)
            {
                case enemyState.idle:
                    //idling movement
                    //sets random time for current movement to happen
                    switchCase = Random.Range(2f, 6f);
                    timer += Time.deltaTime;

                    if (timer >= switchCase)
                    {
                        EnemyIdleMove(Random.Range(0, 5));

                        timer = 0;
                    }

                    anim.SetBool("attack", false);

                    break;

                case enemyState.attack:
                    //move toward player

                    hVel = 0;
                    vVel = 0;

                    anim.SetBool("attack", true);

                    base.EnemyMove();

                    break;
            }
        }
    }
    void EnemyIdleMove(int randChoice)
    {
        switch (randChoice)
        {
            case (0):
                MoveUp();
                break;
            case (1):
                MoveDown();
                break;
            case (2):
                MoveLeft();
                break;
            case (3):
                MoveRight();
                break;
            case (4):
                StopMoving();
                break;
        }
    }
    void MoveUp()
    {
        MovementController(0f, 1f);
    }
    void MoveDown()
    {
        MovementController(0f, -1f);
    }
    void MoveRight()
    {
        MovementController(1f, 0f);
    }
    void MoveLeft()
    {
        MovementController(-1f, 0f);
    }
    void StopMoving()
    {
        MovementController(0f, 0f);
    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        base.OnCollisionEnter2D(col);

        //if enemy collides with something that isn't the player
        if (!col.gameObject.CompareTag("Player"))
        {
            //if in idle state
            if (currentState == enemyState.idle)
            {
                //chooses new direction to move
                EnemyIdleMove(Random.Range(0, 5));
            }

        }
    }

}
