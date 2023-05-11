using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected PlayerController pc;

    [Header("Movement")]
    [SerializeField] protected float moveSpeed = 4f;
    protected float hVel, vVel;

    [Header("Health")]
    [SerializeField] protected int health = 100;
    [SerializeField] protected GameObject coinPrefab;

    protected Color originalColour;


    [Header("AI")]
    [SerializeField] protected float attackRange = 5f;
    protected enum enemyState { idle, attack };
    protected enemyState currentState;

    protected float timer, switchCase, distBetween;


    [Header("Pop Up")]
    [SerializeField] GameObject textPopUp;
    protected Vector3 offset = new Vector3(0, .5f);

    [Header("Damage Player")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float timeBetweenEachHit = 1.25f;
    private float damageTimer;
    [SerializeField] private GameObject hitEffect;


    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        pc = FindObjectOfType<PlayerController>();

        originalColour = sr.color;
    }

    protected virtual void Update()
    {
        TurnToPlayer();
        EnemyMove();


    }


    protected void MovementController(float hor, float ver)
    {
        hVel = hor;
        vVel = ver;
    }


    protected virtual void TurnToPlayer()
    {
        if (transform.position.x > pc.transform.position.x)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }


    public void Damage(int inputDamage)
    {
        FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.enemy, transform.position, .6f);

        //decrement enemy health with the damage of whats hit it
        health -= inputDamage;
        if(health > 0)
        {
            StartCoroutine(DamageColour());

            TextInstantiate();
        }

        //destroy enemy if no health
        if (health <= 0)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);

            Manager.AddKills(1);
            Debug.Log(Manager.kills);

            Destroy(gameObject, 0.01f);

        }
    }

    protected virtual IEnumerator DamageColour()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = originalColour;
    }


    protected virtual void EnemyMove()
    {
        transform.position = Vector2.MoveTowards(transform.position, pc.transform.position, moveSpeed * Time.deltaTime);

    }


    protected virtual void PlayerRange()
    {
        distBetween = Vector2.Distance(transform.position, pc.transform.position);
        if (distBetween <= attackRange)
        {
            currentState = enemyState.attack;
        }
        else
        {
            currentState = enemyState.idle;
        }
    }


    void TextInstantiate()
    {
        var text = Instantiate(textPopUp, transform.position + offset, Quaternion.identity, transform);
        text.GetComponent<TextMeshPro>().text = health.ToString();
    }


    //damage dealth on initial hit
    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Manager.AddHealth(-damage);

            StartCoroutine(FindObjectOfType<CamFollow>().SmallCamShake());

            Instantiate(hitEffect, pc.transform.position, transform.rotation);

            FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.playerDamage, transform.position, 1f);
        }
    }
    //then damage after every x seconds while collided
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= timeBetweenEachHit)
            {
                Manager.AddHealth(-damage);
                damageTimer = 0f;

                StartCoroutine(FindObjectOfType<CamFollow>().SmallCamShake());

                Instantiate(hitEffect, pc.transform.position, transform.rotation);

                FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.playerDamage, transform.position, 1f);
            }


        }
    }


}
