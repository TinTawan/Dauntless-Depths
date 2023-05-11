using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDoor : MonoBehaviour
{
    [SerializeField] private GameObject open, closed;


    private void Start()
    {
        open.SetActive(false);
        closed.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            open.SetActive(true);
            closed.SetActive(false);

            FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.unlockDoor, transform.position, 1f);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            open.SetActive(false);
            closed.SetActive(true);

            FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.unlockDoor, transform.position, 1f);
        }
    }
}
