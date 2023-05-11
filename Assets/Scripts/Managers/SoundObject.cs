using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip clip;
    public float vol = 1f;


    public void StartAudio()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = vol;
        audioSource.Play();

    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
