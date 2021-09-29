using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AchievementType : ScriptableObject
{
    public abstract void CheckAvhievement(PlayerStats _playerStats, AchievementController _controller);
}
