using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Gems Type", menuName = "Achievements Type / GemsType")]
public class AchGemsType : AchievementType
{
    public string achievementTittle;
    public string flappyToUnlock;
    public int achievementIndex;//EL index del logro en el playStats y flappy
    public int gemsGoal;

    public override void CheckAvhievement(PlayerStats _playerStats, AchievementController _controller)
    {
        if (_playerStats.GetTotalGems() >= gemsGoal)
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
