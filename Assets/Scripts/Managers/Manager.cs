using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager instance;
    public static int coins, keys, coinsToUpgrade, kills, endKeys;
    public static float health;

    public static CanvasManager canvas;

    public static bool unlockedBomb;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        canvas = FindObjectOfType<CanvasManager>();

        coins = 0;
        health = 200;
        keys = 0;
        coinsToUpgrade = 100;
        kills = 0;
        endKeys = 0;

        Cursor.visible = false;
        unlockedBomb = false;
    }

    public static void AddCoins(int inputCoins)
    {
        coins += inputCoins;

        if(coins >= coinsToUpgrade)
        {
            canvas.CheckGameState(CanvasManager.GameState.Upgrading);

        }
    }


    public static void AddHealth(float inputHealth)
    {
        health += inputHealth;

        if (health <= 0f)
        {
            canvas.CheckGameState(CanvasManager.GameState.Dead);
        }

    }

    public static void AddKills(int inputKills)
    {
        kills += inputKills;
    }

    public static void AddKeys(int inputKeys)
    {
        keys += inputKeys;

    }
    public static void AddEndKeys(int inputKeys)
    {
        endKeys += inputKeys;
    }

}