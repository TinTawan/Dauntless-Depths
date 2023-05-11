using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class ItemPickUp : MonoBehaviour
{
    protected PlayerController player;
    protected PlayerShoot pShoot;


    [SerializeField] protected GameObject popUpText;


    protected Vector3 offset = new Vector3(0, .5f, 0f);

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        pShoot = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerShoot>();

    }

    //can be overloaded with either a float or a string
    protected virtual void InstantiateText(string inputMessage)
    {
        var text = Instantiate(popUpText, transform.position + offset, Quaternion.identity);
        text.GetComponent<TextMeshPro>().text = inputMessage;
    }
    protected virtual void InstantiateText(float inputVal)
    {
        var text = Instantiate(popUpText, transform.position + offset, Quaternion.identity);
        text.GetComponent<TextMeshPro>().text = inputVal.ToString();
    }


    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.item, transform.position, 1f);

            Destroy(gameObject);
        }
    }
}
