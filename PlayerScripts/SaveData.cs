using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    public bool[] easyLevelsData;
    public bool[] normalLevelsData;
    public bool[] hardLevelsData;
    public string difficultyData;

    public int actualFlappyIndexData;

    public bool[] shopData;
    public bool[] achievementsData;
    public int flappysAmount;

    public int lifesData;
    public int gemsAmountData;
    public int gemsTotalData;
    public ulong lastTimeDie;
    public bool firstDie;
    public ulong lastTimeStoneChange;
    public int hearts;
    public bool extraHeart;

    public int adventurePoints;
    public bool[] recadosData;
    public bool[] AP_RewardsData;

    public SaveData(Data _data)
    {
        easyLevelsData = _data.easyLevelsData;
        normalLevelsData = _data.normalLevelsData;
        hardLevelsData = _data.hardLevelsData;

        actualFlappyIndexData = _data.actualFlappyIndexData;

        shopData = _data.shopData;
        achievementsData = _data.achievementsData;
        flappysAmount = _data.flappysAmount;

        lifesData = _data.lifesData;
        gemsAmountData = _data.gemsAmountData;
        gemsTotalData = _data.gemsTotalData;
        lastTimeDie = _data.lastTimeDie;
        firstDie = _data.firstDie;
        adventurePoints = _data.adventurePoints;
        lastTimeStoneChange = _data.lastTimeStoneChange;
        hearts = _data.heartsData;
        extraHeart = _data.extraHeartData;

        recadosData = _data.recadosData;
        AP_RewardsData = _data.AP_rewardsData;
    }
}
