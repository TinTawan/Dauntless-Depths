using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamFollow : MonoBehaviour
{
    [SerializeField] Transform player, moveToPoint;
    [SerializeField] Vector3 offset;
    [SerializeField] KeyCode camChangeKey = KeyCode.LeftShift;
    [SerializeField] private float moveCamSmoothSpeed = 25f, lookCamSmoothSpeed = 50f;

    //for reference with smooth damp
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private float shakeTime = 1f, shakeAmount = 1f;


    //new inputs
    bool aimIn = false;



    private void Update()
    {
        if (Input.GetKey(camChangeKey) || aimIn)
        {
            LookCam();
        }
        else
        {
            MoveCam();

        }


        var aim = GetComponent<PlayerInput>().actions["Aim"];
        if (aim.IsPressed())
        {
            aimIn = true;
        }
        else
        {
            aimIn = false;
        }


        if (Input.GetKey(KeyCode.LeftBracket))
        {
            GetComponent<AudioSource>().mute = false;
        }
        if (Input.GetKey(KeyCode.RightBracket))
        {
            GetComponent<AudioSource>().mute = true;
        }
    }

    void MoveCam()
    {
        //set cam position to players position and ensure its correct on the z axis with offset
        Vector3 camPos = player.position + offset;
        //use SmoothDamp to move cam so it smoothly follows player
        Vector3 smoothPos = Vector3.SmoothDamp(transform.position, camPos, ref velocity, moveCamSmoothSpeed * Time.deltaTime);

        //Vector3 smoothPos = Vector3.Lerp(transform.position, camPos, moveCamSmoothSpeed * Time.deltaTime);
        transform.position = smoothPos;

    }

    void LookCam()
    {
        //allows player to look around them in a bigger area
        Vector3 camPos = moveToPoint.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, camPos, ref velocity, lookCamSmoothSpeed * Time.deltaTime);
    }


    //move cam to random position inside a sphere around it over a set time a set number of times
    public IEnumerator BigCamShake()
    {
        float timer = 0f;

        //uses while loop and yield return null to run along side delta time
        while (timer < shakeTime)
        {
            timer += Time.deltaTime;
            Vector3 startPos = transform.position + offset;
            //transform.position = startPos + Random.insideUnitSphere * shakeAmount;

            Vector2 shake = Random.insideUnitCircle;
            Vector3 shake3 = new Vector3(shake.x, shake.y, 0);

            transform.position = startPos + shake3 * shakeAmount;

            yield return null;
        }

    }
    public IEnumerator SmallCamShake() //half power cam shake
    {
        float timer = 0f;

        //uses while loop and yield return null to run along side delta time
        while (timer < shakeTime / 2)
        {
            timer += Time.deltaTime;
            Vector3 startPos = transform.position + offset;

            Vector2 shake = Random.insideUnitCircle;
            Vector3 shake3 = new Vector3(shake.x, shake.y, 0);

            transform.position = startPos + shake3 * shakeAmount/2;
            yield return null;
        }

    }

}
