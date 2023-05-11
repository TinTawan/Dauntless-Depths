using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTriggers : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        EndGame();
    }


    protected void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    protected void EndGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

}
