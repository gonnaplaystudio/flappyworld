using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserAch : MonoBehaviour
{
    public Achievement thisAchievement;
    public GameObject achievementImage;
    public TextMeshProUGUI tittle;
    public TextMeshProUGUI description;


    private void Start()
    {
        SetUpAchievement();
    }

    public void SetUpAchievement()
    {
        achievementImage.SetActive(PlayerStats.Instance.
            GetFlappyAchievement(thisAchievement.id));

        switch (PlayerPrefs.GetString("Language", "English"))
        {
            case "English":
                tittle.text = thisAchievement.GetEnglishTittle();
                description.text = thisAchievement.GetEnglishDescription();
                break;

            case "Spanish":
                tittle.text = thisAchievement.GetSpanishTittle();
                description.text = thisAchievement.GetSpanishDescription();
                break;
        }
    }
}
