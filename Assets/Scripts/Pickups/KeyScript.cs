using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : ItemPickUp
{
    [SerializeField] private int keyVal = 1;

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if(CompareTag("EndKey"))
            {
                Manager.AddEndKeys(keyVal);

                FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.endKey, transform.position, 1f);
            }
            else
            {
                Manager.AddKeys(keyVal);

                FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.key, transform.position, 1f);
            }

            InstantiateText("+ key");

            Destroy(gameObject);
            
        }
    }


}
