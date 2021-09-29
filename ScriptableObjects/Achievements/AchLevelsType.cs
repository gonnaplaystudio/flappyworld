using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Levels Type", menuName = "Achievements Type / Levels Type")]
public class AchLevelsType : AchievementType
{
    public string achievementTittle;
    public string flappyToUnlock;
    public int achievementIndex;
    public string levelType;
    public int numberOfLevelsToCheck;//Numero de niveles que chequear
    public int indexToStartChecking;//Por donde empieza a chequear
    private int auxIndex;

    public override void CheckAvhievement(PlayerStats _playerStats, AchievementController _controller)
    {
        //Se suma para un correcto chequeo de niveles al estar todos en un mismo array
        numberOfLevelsToCheck += indexToStartChecking;

        auxIndex = 0;

        switch (levelType)
        {
            case "Easy":
                CheckEasyLevels(_playerStats, _controller);
                break;

            case "Normal":
                CheckNormalLevels(_playerStats, _controller);
                break;

            case "Hard":
                CheckHardLevels(_playerStats, _controller);
                break;
        }        
    }

    public void CheckEasyLevels(PlayerStats _playerStats, AchievementController _controller)
    {
        for (int i = indexToStartChecking; i < numberOfLevelsToCheck; i++)
        {
            if (_playerStats.GetEasyLevel(i))
            {                
                auxIndex++;
            }
        }

        if (auxIndex == numberOfLevelsToCheck)
        {
            _playerStats.SetFlappyAchievements(achievementIndex, true);

            if (!Convert.ToBoolean(PlayerPrefs.GetString(achievementIndex + achievementTittle, "false")))
            {
                SetUpAchievement(_controller);
                PlayerPrefs.SetString(achievementIndex + achievementTittle, "true");
            }
        }
    }

    public void CheckNormalLevels(PlayerStats _playerStats, AchievementController _controller)
    {
        for (int i = indexToStartChecking; i < numberOfLevelsToCheck; i++)
        {
            if (_playerStats.GetNormalLevel(i))
            {
                auxIndex++;
            }
        }

        if (auxIndex == numberOfLevelsToCheck)
        {
            _playerStats.SetFlappyAchievements(achievementIndex, true);

            if (!Convert.ToBoolean(PlayerPrefs.GetString(achievementIndex + achievementTittle, "false")))
            {
                SetUpAchievement(_controller);
                PlayerPrefs.SetString(achievementIndex + achievementTittle, "true");
            }
        }
    }

    public void CheckHardLevels(PlayerStats _playerStats, AchievementController _controller)
    {
        for (int i = indexToStartChecking; i < numberOfLevelsToCheck; i++)
        {
            if (_playerStats.GetHardLevel(i))
            {
                auxIndex++;
            }
        }

        if (auxIndex == numberOfLevelsToCheck)
        {
            _playerStats.SetFlappyAchievements(achievementIndex, true);

            if (!Convert.ToBoolean(PlayerPrefs.GetString(achievementIndex + achievementTittle, "false")))
            {
                SetUpAchievement(_controller);
                PlayerPrefs.SetString(achievementIndex + achievementTittle, "true");
            }
        }
    }

    public void SetUpAchievement(AchievementController _controller)
    {
        _controller.SetUpUnlockAchievement(achievementIndex);
    }
}
