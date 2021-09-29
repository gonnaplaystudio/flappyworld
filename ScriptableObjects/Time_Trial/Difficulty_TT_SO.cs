using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Difficulty_TT", menuName = "Difficulty_TT")]
public class Difficulty_TT_SO : ScriptableObject
{
    [Header("Gems Amount")]
    public int[] easyGems;
    public int[] normalGems;
    public int[] hardGems;

    //Devuelve un array de cantidad de Gemas
    public int[] GetGemsAmount(string _difficultyType)
    {
        switch (_difficultyType)
        {
            case "Easy":
                return easyGems;

            case "Normal":
                return normalGems;

            case "Hard":
                return hardGems;

            default:
                return easyGems;
        }
    }
}
