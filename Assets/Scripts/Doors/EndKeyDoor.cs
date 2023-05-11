using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndKeyDoor : LockedDoorScript
{
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
            if (Manager.endKeys >= keysToOpenDoor)
            {
                doorOpen = true;

                Manager.AddEndKeys(-keysToOpenDoor);
            }
            else
            {
                StartCoroutine(ChangeColour());

            }

        }
    }
    private IEnumerator ChangeColour()
    {
        if (Manager.endKeys < keysToOpenDoor)
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
            itemAmount.text = Manager.endKeys.ToString();

            itemAmountSlider.value = Manager.endKeys;
        }
        else
        {
            fullSliderSection.SetActive(false);
        }

    }



}
