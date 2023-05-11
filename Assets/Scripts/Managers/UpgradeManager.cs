using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private Image[] upgradeImage;
    [SerializeField] private TextMeshProUGUI[] itemNames;
    [SerializeField] private GameObject[] upgrades;
    [SerializeField] private SpriteRenderer[] upgradeSprites;

    private int upgradeChoice;

    private GameObject[] currentUpgrades = new GameObject[3];


    private void Update()
    {
        if(Manager.canvas.currentState == CanvasManager.GameState.Upgrading)
        {
            DisplayUpgrades();
            Manager.canvas.currentState = CanvasManager.GameState.Playing;

        }
    }

    void DisplayUpgrades()
    {
        Manager.AddCoins(-Manager.coinsToUpgrade);

        //loop through 3 times
        for (int i = 0; i < 3; i++)
        {
            upgradeChoice = Random.Range(0, upgrades.Length);

            //set sprite of button to sprite of random upgrade
            upgradeImage[i].sprite = upgradeSprites[upgradeChoice].sprite;
            //set text above button to name of upgrade
            itemNames[i].text = upgrades[upgradeChoice].name;


            //get the 3 random upgrades out into a seperate array to be accessed from canvas script for button choice
            currentUpgrades[i] = upgrades[upgradeChoice];


        }

    }

    public GameObject ReturnUpgradeArray(int inputChoice)
    {
        return currentUpgrades[inputChoice];
    }
     

}
