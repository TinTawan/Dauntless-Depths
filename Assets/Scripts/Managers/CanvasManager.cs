using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CanvasManager : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerShoot playerShoot;
    [SerializeField] private Projectile bullet;
    private MagnetPickUp mag;

    [Header("Game UI Items")]
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI keyText, killsText, endKeyText, healthText;
    [SerializeField] private Slider dashTimerSlider, bombTimeSlider, healthBarSlider, magnetTimeSlider, coinSlider;
    [SerializeField] private Image lockedBomb, bombFillSlider, bombImage, magnetImage;
    [SerializeField] private GameObject magnetSection;

    [Header("UI Sections")]
    [SerializeField] private GameObject gameplayElements;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject upgradeMenu;
    [SerializeField] private GameObject deadMenu;



    //New Inputs
    bool option1, option2, option3;


    public enum GameState
    {
        Playing,
        Paused,
        Upgrading,
        Dead,
        Menu
    }
    public GameState currentState;

    private Color halfOpacity = new Color(1f, 1f, 1f, 0.5f);
    private Color normalColour = new Color(1f, 1f, 1f, 1f);


    private void Awake()
    {
        if(SceneManager.GetActiveScene().name == "Scene1")
        {
            CheckGameState(GameState.Playing);
        }
        else if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            CheckGameState(GameState.Menu);
        }
    }

    private void Start()
    {
        if(currentState == GameState.Playing)
        {

            bombTimeSlider.maxValue = playerShoot.GetBombShootDelay();
            healthBarSlider.maxValue = Manager.health;
            magnetTimeSlider.value = 0f;
            magnetImage.color = halfOpacity;
        }

        

    }

    private void Update()
    {
        if(currentState == GameState.Playing && player != null)
        {
            DisplayCoins();
            DisplayDashTimer();
            DisplayHealth();
            DisplayKeys();
            DisplayKills();
            DisplayMagnetTimer();

            if (Manager.unlockedBomb)
            {
                lockedBomb.enabled = false;
                bombImage.color = normalColour;

                bombFillSlider.enabled = true;
                DisplayBombTimer();
            }
            else
            {
                lockedBomb.enabled = true;
                bombFillSlider.enabled = false;

                bombImage.color = halfOpacity;

            }


            PauseCheck();


            //Upgrades();
        }


    }

    //-----------------Game State---------------------

    public void CheckGameState(GameState inputState)
    {
        currentState = inputState;
        switch (currentState)
        {
            case GameState.Playing:
                PlayingGame();
                break;
            case GameState.Paused:
                PauseGame();
                break;
            case GameState.Upgrading:
                Upgrading();
                break;
            case GameState.Dead:
                PlayerDead();
                break;
            case GameState.Menu:
                MainMenu();
                break;
        }
    }

    void PlayingGame()
    {
        Cursor.visible = false;

        Time.timeScale = 1f;

        gameplayElements.SetActive(true);
        pauseMenu.SetActive(false);
        upgradeMenu.SetActive(false);
        deadMenu.SetActive(false);
    }
    void PauseGame()
    {
        Cursor.visible = true;

        Time.timeScale = 0f;

        gameplayElements.SetActive(false);
        pauseMenu.SetActive(true);
        upgradeMenu.SetActive(false);
        deadMenu.SetActive(false);
    }
    void Upgrading()
    {
        Cursor.visible = true;

        Time.timeScale = 0f;

        gameplayElements.SetActive(false);
        pauseMenu.SetActive(false);
        upgradeMenu.SetActive(true);
        deadMenu.SetActive(false);
    }
    void PlayerDead()
    {
        Cursor.visible = true;

        Time.timeScale = 0f;

        gameplayElements.SetActive(false);
        pauseMenu.SetActive(false);
        upgradeMenu.SetActive(false);
        deadMenu.SetActive(true);

        FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.death, transform.position, 1f);
    }
    void MainMenu()
    {
        Cursor.visible = false;

        Time.timeScale = 1f;

        gameplayElements.SetActive(false);
        pauseMenu.SetActive(false);
        upgradeMenu.SetActive(false);
        deadMenu.SetActive(false);
    }


    void PauseCheck()
    {
        //cant pause if upgrading 
        if ((Gamepad.current.startButton.isPressed || Keyboard.current.escapeKey.isPressed) && !upgradeMenu.activeInHierarchy)
        {
            if (currentState == GameState.Playing)
            {
                CheckGameState(GameState.Paused);
            }
            else if (currentState == GameState.Paused)
            {
                CheckGameState(GameState.Playing);
            }
        }
    }


    //--------------------------------------------------------


    //-------------------Display In Game----------------------

    private void DisplayCoins()
    {
        coinText.text = Manager.coins.ToString();

        coinSlider.maxValue = Manager.coinsToUpgrade;
        coinSlider.value = Manager.coins;
    }


    private void DisplayHealth()
    {
        healthBarSlider.value = Manager.health;

        healthText.text = Manager.health.ToString();
    }

    void DisplayDashTimer()
    {
        dashTimerSlider.maxValue = player.GetTimeTilNextDash();
        dashTimerSlider.value = player.GetDashTimer();
    }

    void DisplayBombTimer()
    {
        bombTimeSlider.value = playerShoot.GetBombTimer();
    }

    void DisplayMagnetTimer()
    {
        //null check for magnet script
        if(FindObjectOfType<MagnetPickUp>() != null)
        {
            mag = GameObject.FindGameObjectWithTag("MagItem").GetComponent<MagnetPickUp>();
            if (mag.GetMagnetTimer() >= mag.GetMaxMagnetTime() - 0.01f)
            {
                magnetTimeSlider.value = 0f;

                magnetImage.color = halfOpacity;
            }
            else
            {
                magnetImage.color = normalColour;

                magnetTimeSlider.value = mag.GetMagnetTimer();
            }
        }

        
    }

    private void DisplayKeys()
    {
        keyText.text = Manager.keys.ToString();
        endKeyText.text = Manager.endKeys.ToString();
    }

    private void DisplayKills()
    {
        killsText.text = Manager.kills.ToString();
    }



    //---------------------------------------------------------


    //----------------------UI Buttons-------------------------

    public void StartButton()
    {
        SceneManager.LoadScene("Scene1");

        Manager.unlockedBomb = false;

        CheckGameState(GameState.Playing);

        bullet.ResetBulletDamage();

        FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.buttonPress, transform.position, 1f);


    }

    public void ResumeButton()
    {
        CheckGameState(GameState.Playing);

        FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.buttonPress, transform.position, 1f);

    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
        CheckGameState(GameState.Menu);

        FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.buttonPress, transform.position, 1f);

    }

    public void QuitButton()
    {
        FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.buttonPress, transform.position, 1f);


        Application.Quit();
    }

    public void ButtonHover()
    {
        FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.buttonHover, transform.position, 1f);

    }

    //====================Upgrade Section=========================

    public void ChooseUpgrade()
    {
        //instantiate chosen upgrade

        //check which of the upgrades was chosen
        if(EventSystem.current.currentSelectedGameObject.name == "option1" || option1)
        {
            Instantiate(upgradeMenu.GetComponent<UpgradeManager>().ReturnUpgradeArray(0), player.transform.position, Quaternion.identity);

            option1 = false;
        }
        if(EventSystem.current.currentSelectedGameObject.name == "option2" || option2)
        {
            Instantiate(upgradeMenu.GetComponent<UpgradeManager>().ReturnUpgradeArray(1), player.transform.position, Quaternion.identity);

            option2 = false;
        }
        if(EventSystem.current.currentSelectedGameObject.name == "option3" || option3)
        {
            Instantiate(upgradeMenu.GetComponent<UpgradeManager>().ReturnUpgradeArray(2), player.transform.position, Quaternion.identity);

            option3 = false;
        }

        Manager.coinsToUpgrade += 10;

        FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.buttonPress, transform.position, 1f);

        CheckGameState(GameState.Playing);

        
        
        
    }


    //============================================================

    //------------------------------------------------------------



    //----------------------NEW INPUTS-----------------------------

    /*void OnPause()
    {
        if (!upgradeMenu.activeInHierarchy)
        {
            if (currentState == GameState.Playing)
            {
                CheckGameState(GameState.Paused);
            }
            else if (currentState == GameState.Paused)
            {
                CheckGameState(GameState.Playing);
            }
        }


    }*/

    void OnOption1()
    {
        if (upgradeMenu.activeInHierarchy)
        {
            //option1 = true;


            Instantiate(upgradeMenu.GetComponent<UpgradeManager>().ReturnUpgradeArray(0), player.transform.position, Quaternion.identity);
        }
        

    }

    void OnOption2()
    {
        if (upgradeMenu.activeInHierarchy)
        {
            option2 = true;

        }
       
    }

    void OnOption3()
    {
        if (upgradeMenu.activeInHierarchy)
        {
            option3 = true;

        }
        
    }



    /*void Upgrades()
    {
        if (upgradeMenu.activeInHierarchy)
        {
            if (option1)
            {
                Instantiate(upgradeMenu.GetComponent<UpgradeManager>().ReturnUpgradeArray(0), player.transform.position, Quaternion.identity);

                option1 = false;
            }
            if (option2)
            {
                Instantiate(upgradeMenu.GetComponent<UpgradeManager>().ReturnUpgradeArray(1), player.transform.position, Quaternion.identity);

                option2 = false;
            }
            if (option3)
            {
                Instantiate(upgradeMenu.GetComponent<UpgradeManager>().ReturnUpgradeArray(2), player.transform.position, Quaternion.identity);

                option3 = false;
            }

            Manager.coinsToUpgrade += 10;

            FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.buttonPress, transform.position, 1f);

            CheckGameState(GameState.Playing);
        }
        else
        {
            option1 = false;
            option2 = false;
            option3 = false;
        }
    }*/

    //-------------------------------------------------------------
}
