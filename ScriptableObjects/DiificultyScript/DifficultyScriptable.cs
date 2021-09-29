using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Difficulty", menuName = "Difficulty")]
public class DifficultyScriptable : ScriptableObject
{
    [Header("Start Velocity")]
    public float easyStartVelocity;
    public float normalStartVelocity;
    public float hardStartVelocity;

    [Header("StartCounters")]
    public int easyStart;
    public int normalStart;
    public int hardStart;

    [Header("Aument Velocity")]
    public int[] easyCounters;
    public int[] normalCounters;
    public int[] hardCounters;

    //Devuelve un array de valores en los que aumenta la velocidad
    public int[] GetIndexDeAumento(string _difficultyType)
    {
        switch (_difficultyType)
        {
            case "Easy":
                return easyCounters;

            case "Normal":
                return normalCounters;

            case "Hard":
                return hardCounters;

            default:
                return easyCounters;
        }
    }

    public int GetStartGoal(string _difficultyType)
    {
        switch (_difficultyType)
        {
            case "Easy":
                return easyStart;

            case "Normal":
                return normalStart;

            case "Hard":
                return hardStart;

            default:
                return 0;
        }
    }

    public float GetStartVelocity(string _difficultyType)
    {
        switch (_difficultyType)
        {
            case "Easy":
                return easyStartVelocity;

            case "Normal":
                return normalStartVelocity;

            case "Hard":
                return hardStartVelocity;

            default:
                return easyStartVelocity;
        }
    }
}
