using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Achievement", menuName = "Achievement")]
public class Achievement : ScriptableObject
{
    public int id;
    public string tittleEN;
    public string descriptionEN;

    public string tittleSPA;
    public string descriptionSPA;

    public string tittleRUS;
    public string descriptionRUS;

    public string GetEnglishTittle()
    {
        return tittleEN;
    }

    public string GetSpanishTittle()
    {
        return tittleSPA;
    }

    public string GetEnglishDescription()
    {
        return descriptionEN;
    }

    public string GetSpanishDescription()
    {
        return descriptionSPA;
    }

    public string GetRussianTittle()
    {
        return tittleRUS;
    }

    public string GetRussianDescription()
    {
        return descriptionRUS;
    }
}
