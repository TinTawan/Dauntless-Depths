using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePickUp : ItemPickUp
{
    [SerializeField] private float addHealthAmount = 10f;
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Manager.AddHealth(addHealthAmount);

            string text = "+ " + addHealthAmount.ToString() + " health";
            InstantiateText(text);

            base.OnTriggerEnter2D(col);
            //Destroy(gameObject);
        }
    }
}
