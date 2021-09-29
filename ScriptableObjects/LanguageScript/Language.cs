using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Language", menuName = "Language")]
public class Language : ScriptableObject
{
    public string[] spanishText;
    public string[] englishText;

    //Index 0
    public string IntroStart()
    {
        switch (PlayerPrefs.GetString("Language", "English"))
        {
            case "English":
                return englishText[0];

            case "Spanish":
                return spanishText[0];

            default:
                return "Error";
        }
    }

    //Index1
    public string MoreLifesPanel1()
    {
        switch (PlayerPrefs.GetString("Language", "English"))
        {
            case "English":
                return englishText[1];

            case "Spanish":
                return spanishText[1];

            default:
                return "Error";
        }
    }

    //Index2
    public string MoreLifesPanel2()
    {
        switch (PlayerPrefs.GetString("Language", "English"))
        {
            case "English":
                return englishText[2];

            case "Spanish":
                return spanishText[2];

            default:
                return "Error";
        }
    }

    //Index3
    public string AhievementTittle()
    {
        switch (PlayerPrefs.GetString("Language", "English"))
        {
            case "English":
                return englishText[3];

            case "Spanish":
                return spanishText[3];

            default:
                return "Error";
        }
    }

    //Index4
    public string AchUser()
    {
        switch (PlayerPrefs.GetString("Language", "English"))
        {
            case "English":
                return englishText[4];

            case "Spanish":
                return spanishText[4];

            default:
                return "Error";
        }
    }

    //Index5
    public string AchCommunity()
    {
        switch (PlayerPrefs.GetString("Language", "English"))
        {
            case "English":
                return englishText[5];

            case "Spanish":
                return spanishText[5];

            default:
                return "Error";
        }
    }

    //Index 6, 7, 8
    public string BuyFlappyText(string _flappyName, int _price)
    {
        switch (PlayerPrefs.GetString("Language", "English"))
        {
            case "English":
                return englishText[6] + " " + _flappyName + " " + englishText[7]
                    + " " + _price.ToString() + " " + englishText[8];

            case "Spanish":
                return spanishText[6] + " " + _flappyName + " " + spanishText[7]
                    + " " + _price.ToString() + " " + spanishText[8];

            default:
                return "Error";
        }
    }

    //Index 9
    public string LockedText()
    {
        switch (PlayerPrefs.GetString("Language", "English"))
        {
            case "English":
                return englishText[9];

            case "Spanish":
                return spanishText[9];

            default:
                return "Error";
        }
    }

    //Index10
    public string GameOverPanel1()
    {
        switch (PlayerPrefs.GetString("Language", "English"))
        {
            case "English":
                return englishText[10];

            case "Spanish":
                return spanishText[10];

            default:
                return "Error";
        }
    }

    //Index11
    public string GameOverPanel2()
    {
        switch (PlayerPrefs.GetString("Language", "English"))
        {
            case "English":
                return englishText[11];

            case "Spanish":
                return spanishText[11];

            default:
                return "Error";
        }
    }

    //Index12
    public string GameOverPanel3()
    {
        switch (PlayerPrefs.GetString("Language", "English"))
        {
            case "English":
                return englishText[12];

            case "Spanish":
                return spanishText[12];

            default:
                return "Error";
        }
    }

    //Index13
    public string VictoryPanel1()
    {
        switch (PlayerPrefs.GetString("Language", "English"))
        {
            case "English":
                return englishText[13];

            case "Spanish":
                return spanishText[13];

            default:
                return "Error";
        }
    }

    //Index14
    public string VictoryPanel2()
    {
        switch (PlayerPrefs.GetString("Language", "English"))
        {
            case "English":
                return englishText[14];

            case "Spanish":
                return spanishText[14];

            default:
                return "Error";
        }
    }

    //Index15
    public string VictoryPanel3()
    {
        switch (PlayerPrefs.GetString("Language", "English"))
        {
            case "English":
                return englishText[15];

            case "Spanish":
                return spanishText[15];

            default:
                return "Error";
        }
    }

    //Index16
    public string GetLanguage()
    {
        switch (PlayerPrefs.GetString("Language", "English"))
        {
            case "English":
                return englishText[16];

            case "Spanish":
                return spanishText[16];

            default:
                return "Error";
        }
    }
}
