using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Amount Type", menuName = "Achievements Type / Amount Type")]
public class AchAmountType : AchievementType
{
    public string achievementTittle;
    public string FlappyToUnlock;
    public int achievementIndex;
    public int achAmountToCheck;
    public int achStartIndex;

    private int auxIndex;

    public override void CheckAvhievement(PlayerStats _playerStats,AchievementController _controller)
    {
        auxIndex = 0;

        for (int i = achStartIndex; i < achAmountToCheck; i++)
        {
            if (_playerStats.GetFlappyAchievement(i))
                auxIndex++;
        }

        if (auxIndex == achAmountToCheck)
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
