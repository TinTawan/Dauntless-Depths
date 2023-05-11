using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPopUp : MenuTriggers
{
    [SerializeField] private GameObject popUpCanvas;


    private void Start()
    {
        popUpCanvas.SetActive(false);
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            popUpCanvas.SetActive(true);

            FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.canvasPopUp, transform.position, 1f);


        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            popUpCanvas.SetActive(false);

        }
    }

}
