using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButtons : MonoBehaviour
{
    [SerializeField] private GameObject[] slides, buttons;
    

    private int currentSlide;


    private void Start()
    {
        currentSlide = 0;
        foreach(GameObject g in slides)
        {
            g.SetActive(false);
        }

        slides[currentSlide].SetActive(true);
    }

    private void OnEnable()
    {
        Cursor.visible = true;
    }
    private void OnDisable()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        if(currentSlide == 0)
        {
            buttons[0].SetActive(false);
        }
        else
        {
            buttons[0].SetActive(true);
        }

        if (currentSlide == slides.Length - 1)
        {
            buttons[1].SetActive(false);
        }
        else
        {
            buttons[1].SetActive(true);
        }
    }

    public void LeftButton()
    {
        if(currentSlide != 0)
        {
            currentSlide--;

            slides[currentSlide + 1].SetActive(false);
            slides[currentSlide].SetActive(true);
        }

        FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.buttonPress, transform.position, 1f);

    }
    public void RightButton()
    {
        if (currentSlide != slides.Length - 1)
        {
            currentSlide++;

            slides[currentSlide - 1].SetActive(false);
            slides[currentSlide].SetActive(true);
        }

        FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.buttonPress, transform.position, 1f);
    }
}