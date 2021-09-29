using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AchievementController : MonoBehaviour
{
    public static AchievementController Instance;

    [Header("Achievement SetUp")]
    public AchievementType[] userAchivements;
    public Achievement[] achievements;
    public int amountChecking;
    public int startCheckingIndex;

    [Header("Achievement Unlock SetUp")]
    public GameObject unlockPanel;
    public Transform instancePosition;
    private TextMeshProUGUI achievementTittle;
    public AudioSource achievementSource;
    public AudioEvent achievementEvent;

    private bool[] selectedAchievements;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Warning: More than one AchievementController Instance.");
            return;
        }

        Instance = this;

        GetAchievements();
        CheckAchievements();
    }

    private void Start()
    {
        
    }

    public void CheckAchievements()
    {
        for (int i = 0; i < userAchivements.Length ; i++)
        {
            if (!selectedAchievements[i])
            {
                userAchivements[i].CheckAvhievement(PlayerStats.Instance, this);
            }
        }
    }

    public void GetAchievements()
    {
        selectedAchievements = new bool[amountChecking];

        for (int i = startCheckingIndex; i < selectedAchievements.Length; i++)
        {
            selectedAchievements[i] = PlayerStats.Instance.GetFlappyAchievement(i);
        }
    }

    public void SetUpUnlockAchievement(int _achievementIndex)
    {
        GameObject panel = Instantiate(unlockPanel, instancePosition);
        achievementTittle = panel.GetComponentInChildren<TextMeshProUGUI>();

        switch (PlayerPrefs.GetString("Language"))
        {
            case "English":
                achievementTittle.text = achievements[_achievementIndex].GetEnglishTittle();
                break;

            case "Spanish":
                achievementTittle.text = achievements[_achievementIndex].GetSpanishTittle();
                break;
        }

        achievementEvent.Play(achievementSource);

        Debug.Log(achievements[_achievementIndex].GetSpanishTittle());
    }
}
