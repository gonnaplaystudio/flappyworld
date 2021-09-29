using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    [Header("Aux VS_Flappys")]
    public Flappy auxLeftFlappy;
    public Flappy auxRightFlappy;

    //VS_Coliseo
    public static Flappy leftFlappy;
    public static Flappy rightFlappy;
    public static string side;

    //Levels complete
    public static bool[] easyLevels;
    public static bool[] normalLevels;
    public static bool[] hardLevels;

    //Levels Difficulty
    private static string difficulty;

    //Locked&Unlocked Flappys
    public static bool[] flappysShop;
    public static bool[] flappysAchievements;
    public static int flappysAmount;

    //Set Selected Flappy
    public Flappy defaultFlappy;
    public static Flappy actualFlappy;

    //Fades Set Up
    public static bool fadeFromLevels;
    private static bool navigationFadeLevel;

    //Player Attributes
    public static int lifes; //LIFES == A ENERGIA
    public static int gemsAmount;
    public static int totalGemsAmount;
    public static ulong lastTimeDie;
    public static bool firstDie;
    public static ulong lastTimeStoneChange;
    public static int hearts;
    public static bool extraHeart;

    //Recados&AP
    public static bool[] recados;
    public static bool[] AP_rewards;
    public static int adventurePoints;

    //Lenguage
    public static string language;
       
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("WARNING : More than one PlayerStats Instance.");
            return;
        }

        Instance = this;
    }

    private void Start()
    {

    }

    #region Hearts

    public int GetHearts()
    {
        return hearts;
    }

    public void SetHearts(int _hearts)
    {
        hearts = _hearts;
    }

    public bool GetExtraHeart()
    {
        return extraHeart;
    }

    public void SetExtraHeart(bool _value)
    {
        extraHeart = _value;
    }

    #endregion

    public void RestLifes()//ENERGIA
    {
        lifes--;
    }

    public void AddLife()//ENERGIA
    {
        lifes++;
    }

    public int GetLifes()//ENERGIA
    {
        return lifes;
    }

    public void SetLifes(int _lifes)//ENERGIA
    {
        lifes = _lifes;
    }

    public ulong GetLastDie()
    {
        return lastTimeDie;
    }

    public void SetLastDie(ulong _value)
    {
        lastTimeDie = _value;
    }

    public void SetDifficulty(string _newDifficulty)
    {
        difficulty = _newDifficulty;
    }

    public string GetDifficulty()
    {
        return difficulty;
    }

    public void SetFistDie(bool _value)
    {
        firstDie = _value;
    }

    public bool GetFirstDie()
    {
        return firstDie;
    }

    //RESETEAR EL CAPITULO
    public void ResetChapter()
    {
        adventurePoints = 0;

        for (int i = 0; i < recados.Length; i++)
        {
            SetRecado(i, false);
        }

        for (int i = 0; i < AP_rewards.Length; i++)
        {
            SetAP_Reward(i, false);
        }
    }

    #region Adventure Points

    public void SetAdventurePoints(int _newStones)
    {
        adventurePoints = _newStones;
    }

    public void AddAdventurePoints(int _value)
    {
        adventurePoints += _value;
    }

    public int GetAdventurePoints()
    {
        return adventurePoints;
    }

    public void SetAP_Reward(int index, bool _value)
    {
        AP_rewards[index] = _value;
    }

    public bool GetAP_Reward(int index)
    {
        return AP_rewards[index];
    }

    public void SetAp_RewardData(bool[] _newData)
    {
        AP_rewards = _newData;
    }

    public bool[] GetAP_RewardData()
    {
        return AP_rewards;
    }

    public void SetNewStoneTime(ulong _newTime)
    {
        lastTimeStoneChange = _newTime;
    }

    public ulong GetStoneTime()
    {
        return lastTimeStoneChange;
    }

    #endregion

    #region Gems

    public int GetGems()
    {
        return gemsAmount;
    }

    public void SetGems(int _newGems)
    {
        gemsAmount += _newGems;
    }

    public int GetTotalGems()
    {
        return totalGemsAmount;
    }

    public void SetTotalGems(int _newGems)
    {
        totalGemsAmount += _newGems;
    }

    #endregion

    #region GetSetFlappy

    public Flappy GetFlappy()
    {
        if(actualFlappy == null)
        {
            return defaultFlappy;
        }

        return actualFlappy;
    }

    public void SetFlappy(Flappy _newFlappy)
    {
        actualFlappy = _newFlappy;
    }

    #endregion

    #region Shop/Achievements

    public bool GetFlappyShop(int _index)
    {
        return flappysShop[_index];
    }

    public void SetFlappyShop(int _index,bool _value)
    {
        flappysShop[_index] = _value;
    }

    public bool GetFlappyAchievement(int _index)
    {
        return flappysAchievements[_index];
    }

    public void SetFlappyAchievements(int _index, bool _value)
    {
        flappysAchievements[_index] = _value;
    }

    public bool[] GetFlappyShopData()
    {
        return flappysShop;
    }

    public void SetFlappyShopData(bool[] _shopData)
    {
        flappysShop = _shopData;
    }

    public bool[] GetAchievementsData()
    {
        return flappysAchievements;
    }

    public void SetAchievementsData(bool[] _achievementsData)
    {
        flappysAchievements = _achievementsData;
    }

    public int GetFlappysAmount()
    {
        return flappysAmount;
    }

    public void SetFlappysAmount(int _flappysAmount)
    {
        flappysAmount = _flappysAmount;
    }

    public void AddFlappysAmount()
    {
        flappysAmount++;
    }

    public void UnlockFlappysPack(int _unlockIndex)
    {
        for (int i = 0; i < _unlockIndex; i++)
        {
            SetFlappyShop(i, true);
        }
    }

    #endregion  

    #region Get/Set Levels

    public bool[] GetEasyLevels()
    {
        return easyLevels;
    }

    public bool[] GetNormalLevels()
    {
        return normalLevels;
    }

    public bool[] GetHardLevels()
    {
        return hardLevels;
    }

    public void SetEasyLevels(bool[] _levels)
    {
        easyLevels = _levels;
    }

    public void SetNormalLevels(bool[] _levels)
    {
        normalLevels = _levels;
    }

    public void SetHardLevels(bool[] _levels)
    {
        hardLevels = _levels;
    }

    public void SetEasyLevel(int _index, bool _value)
    {
        easyLevels[_index] = _value;
    }

    public void SetNormalLevel(int _index, bool _value)
    {
        normalLevels[_index] = _value;
    }

    public void SetHardLevel(int _index, bool _value)
    {
        hardLevels[_index] = _value;
    }

    public bool GetEasyLevel(int _index)
    {
        return easyLevels[_index];
    }

    public bool GetNormalLevel(int _index)
    {
        return normalLevels[_index];
    }

    public bool GetHardLevel(int _index)
    {
        return hardLevels[_index];
    }

    #endregion

    #region FadeSetUp

    public bool GetFadeFromLevels()
    {
        return fadeFromLevels;
    }

    public void SetFadeFromLevels(bool _newValue)
    {
        fadeFromLevels = _newValue;
    }

    public bool GetNavigationFade()
    {
        return navigationFadeLevel;
    }

    public void SetNavigationFade(bool _value)
    {
        navigationFadeLevel = _value;
    }

    #endregion

    #region Recados

    public void SetRecado(int _index, bool value)
    {
        recados[_index] = value;
    }

    public bool GetRecado(int index)
    {
        return recados[index];
    }

    public void SetRecadosData(bool[] _recadosData)
    {
        recados = _recadosData;
    }

    public bool[] GetRecadosData()
    {
        return recados;
    }

    #endregion

    #region VS_Coliseo

    public void SetRightFlappy(Flappy _newFlappy)
    {
        rightFlappy = _newFlappy;
    }

    public void SetLeftFlappy(Flappy _newFlappy)
    {
        leftFlappy = _newFlappy;
    }

    public void SetSide(string _newSide)
    {
        side = _newSide;
    }

    public Flappy GetLeftFlappy()
    {
        if(leftFlappy == null)
        {
            return auxLeftFlappy;
        }
        else
        {
            return leftFlappy;
        }        
    }

    public Flappy GetRightFlappy()
    {
        if(rightFlappy == null)
        {
            return auxRightFlappy;
        }
        else
        {
            return rightFlappy;
        }        
    }

    public string GetSide()
    {
        return side;
    }

    #endregion
}
