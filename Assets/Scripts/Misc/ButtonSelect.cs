using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    [SerializeField] private Button firstButton;


    void Start()
    {
        firstButton.Select();   
    }


}
