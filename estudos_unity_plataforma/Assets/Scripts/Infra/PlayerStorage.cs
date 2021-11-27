using System;
using UnityEngine;

public class PlayerStorage
{
    public static void SaveCoin(int coins)
    {
        PlayerPrefs.SetInt("COIN", coins);
    }

    public static int GetCoins()
    {
        return PlayerPrefs.GetInt("COIN");
    }

    public static void SaveLevel(int level)
    {
        PlayerPrefs.SetInt("LEVEL", level);
    }

    internal static int GetLevel()
    {
        return PlayerPrefs.GetInt("LEVEL");
    }
}