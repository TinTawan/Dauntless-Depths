using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFromTrigger : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCol;
    [SerializeField] private Vector2 colPos;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            boxCol.offset = colPos;
        }
    }
}
