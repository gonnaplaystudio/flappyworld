using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Language", menuName = "TutLanguage")]
public class TutLanguage : ScriptableObject
{
    public string[] spanishText;
    public string[] englishText;

    public string GetTutLanguage(int _index)
    {
        switch (PlayerPrefs.GetString("Language", "English"))
        {
            case "English":
                return englishText[_index];

            case "Spanish":
                return spanishText[_index];

            default:
                return "Error";
        }
    }
}
