using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pointer : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject cursor;

    private Transform pivot;

    private Vector3 mousePos;

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
        }
        

    }

    void MovePointer()
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



        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 cursorPosOffset = new Vector3(0, 0, 10);
        cursor.transform.position = cursorPos + cursorPosOffset;
    }

    void OnLook()
    {
        MovePointer();
    }

}
