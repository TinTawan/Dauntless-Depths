using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] fireworks;

    bool win;

    private void Update()
    {
        if (win)
        {
            StartCoroutine(FireworkSequence());

            win = false;
        }   
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            win = true;

            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void PlayFireworks()
    {
        int randChoice = Random.Range(0, fireworks.Length);

        fireworks[randChoice].Play();


    }

    void LoadEnd()
    {
        Debug.Log("Win");

        SceneManager.LoadScene("EndScene");
    }

    IEnumerator FireworkSequence()
    {
        yield return new WaitForSeconds(1f);

        for(int i = 0; i < fireworks.Length * 2; i++)
        {
            PlayFireworks();

            float randWait = Random.Range(.2f, .6f);

            yield return new WaitForSeconds(randWait);
        }

        yield return new WaitForSeconds(.75f);

        //end sequence
        LoadEnd();

    }

}
