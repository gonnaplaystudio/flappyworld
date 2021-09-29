using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteConfData : MonoBehaviour
{
    public static RemoteConfData Instance;

    //Nombre del capitulo
    private static string cap_Name;

    //Niveles a cargar
    private static int arcadeLevel;
    private static int counterLevel;
    private static int bossLevel;

    //Flappy VS Flappy
    private static bool VS_Level;
    private static string leftFlappy;
    private static string rightFlappy;
    private static string VS_Date;
    private static string leftFlappy_link;
    private static string rightFlappy_link;
    private static string flappy_of_the_week;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("WARNING: More than one RemoteConfData!!");
            return;
        }

        Instance = this;
    }

    public void SetCapName(string newCap_Name)
    {
        cap_Name = newCap_Name;
    }

    public string GetCapName()
    {
        return cap_Name;
    }

    #region GetSetLevels

    public void SetArcadeLevel(int newArcadeLevel)
    {
        arcadeLevel = newArcadeLevel;
    }

    public int GetArcadeLevel()
    {
        return arcadeLevel;
    }

    public void SetCounterLevel(int newCounterLevel)
    {
        counterLevel = newCounterLevel;
    }

    public int GetCounterLevel()
    {
        return counterLevel;
    }

    public void SetBossLevel(int newBossLevel)
    {
        bossLevel = newBossLevel;
    }

    public int GetBossLevel()
    {
        return bossLevel;
    }

    #endregion

    #region Flappy VS Flappy

    public void SetVSLevel(bool _value)
    {
        VS_Level = _value;
    }

    public bool GetVSLevel()
    {
        return VS_Level;
    }

    public void SetLeftFlappy(string newLeftFlappy)
    {
        leftFlappy = newLeftFlappy;
    }

    public string GetLeftFlappy()
    {
        return leftFlappy;
    }

    public void SetRightFlappy(string newRightFlappy)
    {
        rightFlappy = newRightFlappy;
    }

    public string GetRightFlappy()
    {
        return rightFlappy;
    }

    public void SetVSDate(string newVSDate)
    {
        VS_Date = newVSDate;
    }

    public string GetVSDate()
    {
        return VS_Date;
    }

    public void SetLeftFlappyLink(string newLeftFlappyLink)
    {
        leftFlappy_link = newLeftFlappyLink;
    }

    public string GetLeftFlappyLink()
    {
        return leftFlappy_link;
    }

    public void SetRightFlappyLink(string newRightFlappyLink)
    {
        rightFlappy_link = newRightFlappyLink;
    }

    public string GetRightFlappyLink()
    {
        return rightFlappy_link;
    }

    public void SetFlappyOfTheWeek(string newFlappyOfTheWeek)
    {
        flappy_of_the_week = newFlappyOfTheWeek;
    }

    public string GetFlappyOfTheWeek()
    {
        return flappy_of_the_week;
    }

    #endregion
}
