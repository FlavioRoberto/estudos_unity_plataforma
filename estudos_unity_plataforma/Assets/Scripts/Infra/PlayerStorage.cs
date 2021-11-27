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
}