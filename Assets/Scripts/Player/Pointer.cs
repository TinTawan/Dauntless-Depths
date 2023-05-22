using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pointer : MonoBehaviour
{
    [SerializeField] Transform player, cursorPos;
    [SerializeField] GameObject cursor;

    private Transform pivot;

    private Vector3 mousePos;


    //New Inputs
    [SerializeField] float contDeadZone = 0.1f;
    [SerializeReference] float contRotateSpeed = 1000f;

    private Controls playerControls;
    private PlayerInput playerInput;

    Vector2 aim;

    private void Awake()
    {
        playerControls = new Controls();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        //set transform as a child and position of pivot on start
        pivot = player.transform;
        transform.parent = pivot;
        transform.position += Vector3.up;

        //lock and hide cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        
    }

    private void Update()
    {
        //can only move gun if in playing state
        if (Manager.canvas.currentState == CanvasManager.GameState.Playing)
        {
            MovePointer();

            aim = playerControls.Player.Look.ReadValue<Vector2>();

            cursor.transform.position = cursorPos.position;
        }


        if(playerInput.currentControlScheme == "Gamepad")
        {
            Manager.isGamepad = true;
        }
        else
        {
            Manager.isGamepad = false;
        }
    }

    void MovePointer()
    {

        if (!Manager.isGamepad)
        {
            //get mouse position compared to player
            mousePos = Camera.main.WorldToScreenPoint(player.position);
            mousePos = Input.mousePosition - mousePos;
            //find angle from player to mouse
            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

            //move pointer with player
            transform.position = player.position;
            //rotate pointer with given angle on the z axis
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);


            
        }
        else
        {
            
            if(Mathf.Abs(aim.x) > contDeadZone || Mathf.Abs(aim.y) > contDeadZone)
            {
                Vector2 direction = new Vector2(aim.x, aim.y);
                if(direction.sqrMagnitude > 0.0f)
                {
                    Quaternion rotate = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotate, contRotateSpeed * Time.deltaTime);

                }
            }
        }


        
    }

    //------------NEW INPUT---------------------

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }


}
