using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static Data Instance;

    [Header("Levels")]
    public bool[] easyLevelsData;
    public bool[] normalLevelsData;
    public bool[] hardLevelsData;
    public string difficultyData;

    [Header("Actual Flappy")]
    public Flappy[] allFlappys;
    public int actualFlappyIndexData;

    [Header("Shop/Achievements")]
    public bool[] shopData;
    public bool[] achievementsData;
    public int flappysAmount;

    [Header("Stats")]
    public int lifesData;
    public int gemsAmountData;
    public int gemsTotalData;
    public ulong lastTimeDie;
    public bool firstDie;
    public ulong lastTimeStoneChange;
    public int heartsData;
    public bool extraHeartData;

    [Header("Recados&AP")]
    public bool[] recadosData;
    public bool[] AP_rewardsData;
    public int adventurePoints;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("WARNING: More than one Data Instance.");
            return;
        }

        Instance = this;
    }

    public void CreateData()
    {
        SaveSystem.SaveData(this);
    }

    public void SaveData()
    {
        GetPlayerStatsValues(PlayerStats.Instance);
        SaveSystem.SaveData(this);
    }

    public void LoadData()
    {
        GetSaveData();
        SetPlayersStatsValues(PlayerStats.Instance);
    }

    public void GetPlayerStatsValues(PlayerStats _playerStats)
    {
        easyLevelsData = _playerStats.GetEasyLevels();
        normalLevelsData = _playerStats.GetNormalLevels();
        hardLevelsData = _playerStats.GetHardLevels();

        actualFlappyIndexData = _playerStats.GetFlappy().id;

        lifesData = _playerStats.GetLifes();
        gemsAmountData = _playerStats.GetGems();
        gemsTotalData = _playerStats.GetTotalGems();
        lastTimeDie = _playerStats.GetLastDie();
        firstDie = _playerStats.GetFirstDie();
        adventurePoints = _playerStats.GetAdventurePoints();
        lastTimeStoneChange = _playerStats.GetStoneTime();
        heartsData = _playerStats.GetHearts();
        extraHeartData = _playerStats.GetExtraHeart();

        shopData = _playerStats.GetFlappyShopData();
        achievementsData = _playerStats.GetAchievementsData();

        flappysAmount = _playerStats.GetFlappysAmount();

        recadosData = _playerStats.GetRecadosData();
        AP_rewardsData = _playerStats.GetAP_RewardData();
    }

    public void SetPlayersStatsValues(PlayerStats _playerStats)
    {
        _playerStats.SetEasyLevels(easyLevelsData);
        _playerStats.SetNormalLevels(normalLevelsData);
        _playerStats.SetHardLevels(hardLevelsData);

        _playerStats.SetFlappy(GetFlappyData(actualFlappyIndexData));

        _playerStats.SetLifes(lifesData);
        _playerStats.SetGems(gemsAmountData);
        _playerStats.SetTotalGems(gemsTotalData);
        _playerStats.SetLastDie(lastTimeDie);
        _playerStats.SetFistDie(firstDie);
        _playerStats.SetAdventurePoints(adventurePoints);
        _playerStats.SetNewStoneTime(lastTimeStoneChange);
        _playerStats.SetHearts(heartsData);
        _playerStats.SetExtraHeart(extraHeartData);

        _playerStats.SetFlappyShopData(shopData);
        _playerStats.SetAchievementsData(achievementsData);

        _playerStats.SetFlappysAmount(flappysAmount);

        _playerStats.SetRecadosData(recadosData);

        _playerStats.SetAp_RewardData(AP_rewardsData);
    }

    public Flappy GetFlappyData(int _flappyIndex)
    {
        for (int i = 0; i < allFlappys.Length; i++)
        {
            if (allFlappys[i].id == actualFlappyIndexData)
                return allFlappys[i];
                        
        }

        return allFlappys[0];
    }

    public void GetSaveData()
    {
        SaveData newSaveData = SaveSystem.LoadData();

        for (int i = 0; i < newSaveData.easyLevelsData.Length; i++)
        {
            easyLevelsData[i] = newSaveData.easyLevelsData[i];
            normalLevelsData[i] = newSaveData.normalLevelsData[i];
            hardLevelsData[i] = newSaveData.hardLevelsData[i];
        }

        for (int i = 0; i < newSaveData.shopData.Length; i++)
        {
            shopData[i] = newSaveData.shopData[i];
        }

        for (int i = 0; i < newSaveData.achievementsData.Length; i++)
        {
            achievementsData[i] = newSaveData.achievementsData[i];
        }

        SetRecadosData(newSaveData);
        SetAP_RewardsData(newSaveData);

        flappysAmount = newSaveData.flappysAmount;

        actualFlappyIndexData = newSaveData.actualFlappyIndexData;

        lifesData = newSaveData.lifesData;
        gemsAmountData = newSaveData.gemsAmountData;
        gemsTotalData = newSaveData.gemsTotalData;
        lastTimeDie = newSaveData.lastTimeDie;
        firstDie = newSaveData.firstDie;
        adventurePoints = newSaveData.adventurePoints;
        lastTimeStoneChange = newSaveData.lastTimeStoneChange;
        heartsData = newSaveData.hearts;
        extraHeartData = newSaveData.extraHeart;
    }

    public void SetRecadosData(SaveData newSaveData)
    {
        for (int i = 0; i < newSaveData.recadosData.Length; i++)
        {
            recadosData[i] = newSaveData.recadosData[i];
        }
    }

    public void SetAP_RewardsData(SaveData newSaveData)
    {
        for (int i = 0; i < newSaveData.AP_RewardsData.Length; i++)
        {
            AP_rewardsData[i] = newSaveData.AP_RewardsData[i];
        }
    }
}
