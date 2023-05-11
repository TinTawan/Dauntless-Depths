using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillDoor : LockedDoorScript
{

    [SerializeField] private int killToOpenDoor = 100;

    protected override void Start()
    {
        base.Start();

        maxItemsForDoor.text = killToOpenDoor.ToString();
        itemAmountSlider.maxValue = killToOpenDoor;
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
            if (Manager.kills >= killToOpenDoor)
            {
                doorOpen = true;

                FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.unlockDoor, transform.position, 1f);

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
        if (Manager.kills < killToOpenDoor)
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
            itemAmount.text = Manager.kills.ToString();

            itemAmountSlider.value = Manager.kills;
        }
        else
        {
            fullSliderSection.SetActive(false);
        }

    }
}
