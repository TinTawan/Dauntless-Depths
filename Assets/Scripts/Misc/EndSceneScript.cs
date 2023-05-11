using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneScript : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void ButtonHover()
    {
        FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.buttonHover, transform.position, 1f);

    }

}
