using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneNames
{
    menu,
    lvl1,
    lvl2,
    lvl3
}

public enum PlayerColors
{
    Yellow,
    Orange,
    Purple,
    Mint,
    Green,
    Gray,
    Pink,
    Blue
}

public class PlayerSettings : MonoBehaviour
{
    public static PlayerSettings Instance { get; set; }
    private SceneNames lastLevel;
    public SceneNames LastLevel
    {
        get { return lastLevel; }
        set
        {
            lastLevel = value;
            SaveLastLevel(lastLevel);
        }
    }

    private bool sound;
    public bool Sound
    {
        get { return sound; }
        set
        {
            sound = value;
            SaveSound(sound);
        }
    }

    private PlayerColors color;
    public PlayerColors Color
    {
        get { return color; }
        set
        {
            color = value;
            SaveColor(color);
        }
    }

    private int wowCounter;
    public int WowCounter
    {
        get { return wowCounter; }
        set
        {
            wowCounter = value;
            if (wowCounter == 50)
                Coins += 25;
            SaveWowCount(wowCounter);            
        }
    }

    private int wowMultiplier;
    public int WowMultiplier
    {
        get { return wowMultiplier; }
        set
        {
            wowMultiplier = value;
            if (wowMultiplier == 16)
            {
                Coins += 50;
                //Check if already reached
            }    
            SaveWowCount(wowMultiplier);
        }
    } 

    private int deaths;
    public int Deaths
    {
        get { return deaths; }
        set
        {
            if (deaths == 50)
                Coins += 5;
            deaths = value;
            SaveDeaths(deaths);
        }
    }

    private int coins;
    public int Coins
    {
        get { return coins; }
        set
        {
            coins = value;
            SaveCoins(coins);
        }
    }

    private int wallsDestoyed;
    public int WallsDestoyed
    {
        get { return wallsDestoyed; }
        set
        {
            wallsDestoyed = value;
            if (wallsDestoyed == 100)
                Coins += 100;
            SaveWallsDestoyed(wallsDestoyed);
        }
    }


    private void Awake()
    {
        lastLevel = GetLastLevel();
        sound = GetSound();
        color = GetColor();
        wowCounter = GetWowCount();
        wowMultiplier = GetWowMultiplier();
        deaths = GetDeaths();
        wallsDestoyed = GetWallsDestoyed();
        coins = GetCoins();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this);
        
    }

    private int GetCoins()
    {
        return PlayerPrefs.GetInt("Coins", 0);
    }

    private void SaveCoins(int count)
    {
        PlayerPrefs.SetInt("Coins", count);
    }
    private int GetWallsDestoyed()
    {
        return PlayerPrefs.GetInt("WallsDestoyed", 0);
    }

    private void SaveWallsDestoyed(int count)
    {
        PlayerPrefs.SetInt("WallsDestoyed", count);
    }
    private int GetDeaths()
    {
        return PlayerPrefs.GetInt("Deaths", 0);
    }

    private void SaveDeaths(int count)
    {
        PlayerPrefs.SetInt("Deaths", count);
    }
    private int GetWowMultiplier()
    {
        return PlayerPrefs.GetInt("WowMultiplier", 0);
    }

    private void SaveWowMultiplier(int count)
    {
        PlayerPrefs.SetInt("WowMultiplier", count);
    }
    private int GetWowCount()
    {
        return PlayerPrefs.GetInt("WowCounter", 0);
    }

    private void SaveWowCount(int count)
    {
        PlayerPrefs.SetInt("WowCounter", count);
    }

    private SceneNames GetLastLevel()
    {
        return (SceneNames)PlayerPrefs.GetInt("LastLevel", 1);
    }

    private void SaveLastLevel(SceneNames sceneName)
    {
        PlayerPrefs.SetInt("LastLevel", (int)sceneName);
    }

    private PlayerColors GetColor()
    {
        return (PlayerColors)PlayerPrefs.GetInt("PlayerColor", 1);
    }

    private void SaveColor(PlayerColors color)
    {
        PlayerPrefs.SetInt("PlayerColor", (int)color);
    }

    private bool GetSound()
    {
        var sound = PlayerPrefs.GetInt("Sound", 1);
        return IntToBool(sound);
    }

    private void SaveSound(bool sound)
    {
        PlayerPrefs.SetInt("Sound", BoolToInt(sound));
    }

    /// <summary>
    /// 1 - true, other - false
    /// </summary>
    private bool IntToBool(int i)
    {
        return i == 1;
    }

    private int BoolToInt(bool b)
    {
        return b ? 1 : 0;
    }
}
