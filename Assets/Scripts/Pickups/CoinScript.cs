using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : ItemPickUp
{
    [SerializeField] private int coinVal = 10;

    private float timer, timeToDestroy = 1f;
    private bool destroy = false;

    [SerializeField] private float magnestiseSpeed = 2f;
    [SerializeField] private float distToPlayer = 2f;


    private void Update()
    {

        //so gameObject disapears when collected and then destroys after text has appeared
        if (destroy)
        {
            timer += Time.deltaTime;
            if (timer >= timeToDestroy)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        else
        {
            MoveToPlayer();
        }
        
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !destroy)
        {
            timer = 0f;
            destroy = true;

            InstantiateText(coinVal);
            Manager.AddCoins(coinVal);

            FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.coin, transform.position, 1f);
        }
    }


    void MoveToPlayer()
    {
        float dist = Vector2.Distance(gameObject.transform.position, player.transform.position);
        if(dist < distToPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, magnestiseSpeed * Time.deltaTime);
        }
    }

    public void SetDistToPlayer(float inputFloat)
    {
        distToPlayer = inputFloat;
    } 
}
