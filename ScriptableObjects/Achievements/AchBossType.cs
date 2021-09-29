using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Boss Type", menuName = "Achievements Type / Boss Type")]
public class AchBossType : AchievementType
{
    public string achievementTittle;
    public string flappyToUnlock;
    public int achievementIndex;
    public int bossLevelIndex;

    public override void CheckAvhievement(PlayerStats _playerStats, AchievementController _controller)
    {
        if(_playerStats.GetEasyLevel(bossLevelIndex) || _playerStats.GetNormalLevel(bossLevelIndex)
            || _playerStats.GetHardLevel(bossLevelIndex))
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
