using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyText : MonoBehaviour
{
    [SerializeField] float destroyTime = 2f;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
