using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyDoor : LockedDoorScript
{
    [SerializeField] private BoxCollider2D otherDoorCol;

    [SerializeField] private int keysToOpenDoor = 10;


    protected override void Start()
    {
        base.Start();

        maxItemsForDoor.text = keysToOpenDoor.ToString();
        itemAmountSlider.maxValue = keysToOpenDoor;
    }

    protected override void Update()
    {
        base.Update();

        UpdateKeyUI();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (Manager.keys >= keysToOpenDoor)
            {
                doorOpen = true;

                Manager.AddKeys(-keysToOpenDoor);

                FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.unlockDoor, transform.position, 1f);

                GetComponent<BoxCollider2D>().enabled = false;

                OpenOtherDoor();
            }
            else
            {
                StartCoroutine(ChangeColour());

                FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.lockDoor, transform.position, 1f);

            }

        }
    }
    private IEnumerator ChangeColour()
    {
        if (Manager.keys < keysToOpenDoor)
        {
            for (int i = 0; i < 3; i++)
            {
                foreach (SpriteRenderer door in doorSprites)
                {
                    door.color = Color.red;
                }
                yield return new WaitForSeconds(.15f);
                foreach (SpriteRenderer door in doorSprites)
                {
                    door.color = Color.white;
                }
                yield return new WaitForSeconds(.15f);
            }

        }

    }

    void UpdateKeyUI()
    {
        if (!doorOpen)
        {
            itemAmount.text = Manager.keys.ToString();

            itemAmountSlider.value = Manager.keys;
        }
        else
        {
            fullSliderSection.SetActive(false);
        }

    }

    private void OpenOtherDoor()
    {
        otherDoorCol.offset = new Vector2(0f, -0.6f);
    }
}
