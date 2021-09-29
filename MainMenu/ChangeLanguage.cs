using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Optionsss : MonoBehaviour
{
    [Header("Option Texts")]
    public TextMeshProUGUI[] optionsTexts;

    [Header("Language SetUp")]
    public TutLanguage languageArchive;
    public string[] languagesSaveString;
    private int index;

    private void Awake()
    {
        GetIndex();
        SetUpLangage();
    }

    public void LanguageButton()
    {
        Change();
        SetUpLangage();
    }

    public void GetIndex()
    {
        index = PlayerPrefs.GetInt("LanguageIndex", 0);
    }

    public void SetUpLangage()
    {
        for (int i = 0; i < optionsTexts.Length; i++)
        {
            optionsTexts[i].text = languageArchive.GetTutLanguage(i);
        }
    }

    public void Change()
    {
        index++;

        if (index == languagesSaveString.Length)
            index = 0;

        switch (index)
        {
            case 0:
                PlayerPrefs.SetString("Language", languagesSaveString[index]);
                break;

            case 1:
                PlayerPrefs.SetString("Language", languagesSaveString[index]);
                break;
        }


        PlayerPrefs.SetInt("LanguageIndex", index);
    }
}
