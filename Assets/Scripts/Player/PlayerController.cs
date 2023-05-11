using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Animator anim;
    [SerializeField] private TrailRenderer dashTrail;
    [SerializeField] private GameObject hitEffect;

    [Header("Input Section")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float dashMult = 2f;
    [SerializeField] private float dashTime = 1f;
    [SerializeField] private float timeTilNextDash = 5f;

    [SerializeField] private KeyCode dashButton = KeyCode.Space;


    private float hVel, vVel, dashTimer, nextDashTimer;
    private bool faceRight, dashing = false;

    float stepTime = 0.3f , nextStep;


    private void Start()
    {
        dashTrail.enabled = false;
    }

    private void Update()
    {
        MovePlayer();

        DashInputs();

        Cheats();

    }

    void MovePlayer()
    {
        //get inputs for hori and vert velocity
        hVel = Input.GetAxisRaw("Horizontal");
        vVel = Input.GetAxisRaw("Vertical");

        //set animator values for paramaters to these inputs
        anim.SetFloat("xVel", Mathf.Abs(hVel)); //absolute value as I use a flip to swap direction
        anim.SetFloat("yVel", vVel);
        //used a blend tree

        if (hVel == 0 && vVel == 0)
        {
            anim.SetBool("stoodStill", true);
        }
        else 
        {
            anim.SetBool("stoodStill", false);
        }


        if (rb.velocity.x > 0.1 && faceRight)
        {
            FlipPlayer();
        }
        else if (rb.velocity.x < -0.1 && !faceRight)
        {
            FlipPlayer();
        }
    }

    void DashInputs()   
    {
        //increment timer 
        nextDashTimer += Time.deltaTime;
        //only can dash if timer has reached the right time
        if(nextDashTimer >= timeTilNextDash)
        {
            //if not dashing and press dash button
            if (!dashing && Input.GetKeyDown(dashButton))
            {
                FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.dash, transform.position, .7f);
                //dash in direction that player is moving
                //reset timer

                //dash straight
                if (Input.GetKey(KeyCode.W))
                {
                    StartCoroutine(Dash(Vector2.up));

                    nextDashTimer = 0f;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    StartCoroutine(Dash(Vector2.left));

                    nextDashTimer = 0f;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    StartCoroutine(Dash(Vector2.down));

                    nextDashTimer = 0f;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    StartCoroutine(Dash(Vector2.right));

                    nextDashTimer = 0f;
                }
                //dash diagonally (root 2 so dashing diagonally is same distance as normal)
                if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
                {
                    StartCoroutine(Dash(new Vector2(Mathf.Sqrt(2), Mathf.Sqrt(2))));
                    

                    nextDashTimer = 0f;
                }
                if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
                {
                    StartCoroutine(Dash(new Vector2(-Mathf.Sqrt(2), Mathf.Sqrt(2))));

                    nextDashTimer = 0f;
                }
                if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
                {
                    StartCoroutine(Dash(new Vector2(Mathf.Sqrt(2), -Mathf.Sqrt(2))));

                    nextDashTimer = 0f;
                }
                if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
                {
                    StartCoroutine(Dash(new Vector2(-Mathf.Sqrt(2), -Mathf.Sqrt(2))));

                    nextDashTimer = 0f;
                }
            }
            
        } 
        

        
    }

    private void FixedUpdate()
    {
        //use normal movement when not dashing
        //dashing affects velocity so must be seperate
        if (!dashing)   
        {
            Vector2 moveVector = new Vector2(hVel, vVel);
            Vector2 moveVel = moveVector.normalized;
            rb.velocity = moveVel * moveSpeed;

            if(hVel + vVel != 0)
            {
                nextStep += Time.deltaTime;
                if (nextStep >= stepTime)
                {
                    FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.walk, transform.position, 1f);
                    nextStep = 0f;
                }
            }
            
        }
        

    }

    private void FlipPlayer()
    {
        //player faces the opposite way and multiply player's local scale by -1 in the x axis
        faceRight = !faceRight;

        Vector3 flip = transform.localScale;
        flip.x *= -1;
        transform.localScale = flip;

    }


    IEnumerator Dash(Vector2 facing)
    {

        dashing = true;
        dashTrail.enabled = true;

        dashTimer = 0f;

        while (dashTimer < dashTime)    //while loop to run alongside update()
        {
            dashTimer += Time.deltaTime;

            rb.velocity = facing * dashMult * moveSpeed;   //move player in direction theyre facing with given speed

            yield return null;  //wait until next frame in update
        }
        rb.velocity = new Vector2(0f, 0f);  //set player velocity back to 0 so movement is normal

        
        dashing = false;

        yield return new WaitForSeconds(0.5f);
        dashTrail.enabled = false;
    }


    public float GetDashTimer()
    {
        return nextDashTimer;
    }
    public float GetTimeTilNextDash()
    {
        return timeTilNextDash;
    }
    public void AddDashCooldown(float inputTime)
    {
        timeTilNextDash += inputTime;
    }
    public void AddSpeed(float inputSpeed, float inputStepTime)
    {
        moveSpeed += inputSpeed;
        stepTime -= inputStepTime;
    }

    //-------------------Cheats---------------------
    void Cheats()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Manager.AddCoins(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Manager.AddHealth(150f-Manager.health);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Manager.unlockedBomb = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Manager.AddKills(10);
        }
    }
    //-------------------------------------------------

}