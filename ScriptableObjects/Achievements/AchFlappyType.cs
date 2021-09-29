using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Flappys Type", menuName = "Achievements Type / Flappys Type")]
public class AchFlappyType : AchievementType
{
    public string achievementTittle;
    public string flappyToUnlock;
    public int achievementIndex;//El index del logro que desbloquea
    public int flappyAmountGoal;

    public override void CheckAvhievement(PlayerStats _playerStats, AchievementController _controller)
    {
        if(_playerStats.GetFlappysAmount() >= flappyAmountGoal)
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
