using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gems_TT_Controller : MonoBehaviour
{
    [Header("Gems Particles")]
    public Transform value_1_Container;
    public Transform value_2_Container;
    public Transform value_3_Container;

    public TextMeshProUGUI gemsText;

    public Transform actualPointsContainer;
    public Transform[] spawnPointContainers;
    public Transform gemsContainer;

    public Transform[] gemSpawnPoints;
    public GameObject[] gems_obj;
    public Gem_TT[] gems_TT;
    public Iman_TT[] imanes_TT;
    public Time_Trial time_Trial;
    public Difficulty_TT_SO difficulty_TT_SO;
    public string actualDifficulty;

    private int gemsAmount;
    private int gemsIndex;

    public int greenGemAmount;
    public int redGemAmount;
    public int purpleGemAmount;

    public int greenGemValue;
    public int redGemValue;
    public int purpleGemValue;

    public List<int> randomNumbers;

    private void Awake()
    {
        actualDifficulty = PlayerStats.Instance.GetDifficulty();
        CheckDifficulty(actualDifficulty);
        StartSetUp();
    }

    public void CheckDifficulty(string _difficulty)
    {
        switch (_difficulty)
        {
            case "Easy":
                actualPointsContainer = spawnPointContainers[0];
                SetUpGemsAmount(difficulty_TT_SO.GetGemsAmount("Easy"));
                break;

            case "Normal":
                actualPointsContainer = spawnPointContainers[1];
                SetUpGemsAmount(difficulty_TT_SO.GetGemsAmount("Normal"));
                break;

            case "Hard":
                actualPointsContainer = spawnPointContainers[2];
                SetUpGemsAmount(difficulty_TT_SO.GetGemsAmount("Hard"));
                break;

            default:
                actualPointsContainer = spawnPointContainers[0];
                SetUpGemsAmount(difficulty_TT_SO.GetGemsAmount("Easy"));
                break;
        }
    }

    public void SetUpGemsAmount(int[] _gemsAmount)
    {
        greenGemAmount = _gemsAmount[0];
        redGemAmount = _gemsAmount[1];
        purpleGemAmount = _gemsAmount[2];
    }

    public void StartSetUp()
    {
        gemSpawnPoints = new Transform[actualPointsContainer.childCount];
        gems_obj = new GameObject[gemsContainer.childCount];
        imanes_TT = new Iman_TT[gems_obj.Length];
        gems_TT = new Gem_TT[gems_obj.Length];

        for (int i = 0; i < gemSpawnPoints.Length; i++)
        {
            gemSpawnPoints[i] = actualPointsContainer.GetChild(i).transform;
            gems_obj[i] = gemsContainer.GetChild(i).gameObject;
            imanes_TT[i] = gems_obj[i].GetComponent<Iman_TT>();
            imanes_TT[i].SetStartPos(gemSpawnPoints[i]);
            gems_TT[i] = imanes_TT[i].GetThisGem();

            gems_TT[i].SetValue_1(value_1_Container.GetChild(i).GetComponent<ParticleSystem>());
            gems_TT[i].SetValue_2(value_2_Container.GetChild(i).GetComponent<ParticleSystem>());
            gems_TT[i].SetValue_3(value_3_Container.GetChild(i).GetComponent<ParticleSystem>());
        }

        SetRandomGems();
    }


    public void CheckDesactive()
    {
        gemsIndex++;

        if(gemsIndex >= gems_obj.Length)
        {
            //Se activa el black hole
            time_Trial.SetActiveHole(true);
            time_Trial.SetUpEndHole();
            //Se para el tiempo
            time_Trial.StopTime();
        }
    }

    public void SetRandomGems()
    {
        SetGreenGems();
        SetRedGems();
        SetPurpleGems();
    }    

    public void SetGreenGems()
    {
        for (int i = 0; i < greenGemAmount; i++)
        {
            gems_TT[GetRandomNumber()].SetGemValue("gem_green", greenGemValue);
        }
    }

    public void SetRedGems()
    {
        for (int i = 0; i < redGemAmount; i++)
        {
            gems_TT[GetRandomNumber()].SetGemValue("gem_red", redGemValue);
        }
    }

    public void SetPurpleGems()
    {
        for (int i = 0; i < purpleGemAmount; i++)
        {
            gems_TT[GetRandomNumber()].SetGemValue("gem_purple", purpleGemValue);
        }
    }

    public void ActiveGems()
    {
        for (int i = 0; i < gems_obj.Length; i++)
        {
            gems_obj[i].SetActive(true);
        }

        gemsIndex = 0;
    }

    public void DesactiveGems()
    {
        for (int i = 0; i < gems_obj.Length; i++)
        {
            gems_obj[i].SetActive(false);
        }
    }

    public int GetRandomNumber()
    {
        int randomNumber = (int)Random.Range(0f, gems_obj.Length);

        while (randomNumbers.Contains(randomNumber))
        {
            randomNumber = (int)Random.Range(0f, gems_obj.Length);
        }

        randomNumbers.Add(randomNumber);

        return randomNumber;
    }

    public void AddGem(int _value)
    {
        gemsAmount += _value;
        SetUpGemsText();
    }

    public void SetGemsAmount(int _gemsAmount)
    {
        gemsAmount = _gemsAmount;
    }

    public int GetGemsAmount()
    {
        return gemsAmount;
    }

    public void SetUpGemsText()
    {
        gemsText.text = gemsAmount.ToString();
    }
}
