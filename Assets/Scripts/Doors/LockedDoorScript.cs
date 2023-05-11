using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class LockedDoorScript : MonoBehaviour
{
    [Header("DoorSection")]
    [SerializeField] private GameObject openDoor;
    [SerializeField] private GameObject closedDoor;
    [SerializeField] protected SpriteRenderer[] doorSprites;
    protected bool doorOpen;


    [Header("EnemySpawner")]
    [SerializeField] private GameObject spawnArea;
    Spawning spawnScript;

    [Header("Slider")]
    [SerializeField] protected Slider itemAmountSlider;
    [SerializeField] protected TextMeshPro itemAmount;
    [SerializeField] protected TextMeshPro maxItemsForDoor;
    [SerializeField] protected GameObject fullSliderSection;

    protected virtual void Start()
    {
        //set correct doors to show
        openDoor.SetActive(false);
        closedDoor.SetActive(true);

        spawnScript = spawnArea.GetComponent<Spawning>();
    }

    protected virtual void Update()
    {
        if (doorOpen)
        {
            OpenDoor();
            spawnScript.CancelInvoke("SpawnInMultipleBoxes");
        }
        else
        {
            ClosedDoor();
        }

        
    }


    void OpenDoor()
    {
        openDoor.SetActive(true);
        closedDoor.SetActive(false);
    }
    void ClosedDoor()
    {
        openDoor.SetActive(false);
        closedDoor.SetActive(true);
    }


    


}
