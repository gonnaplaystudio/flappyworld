using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gem : MonoBehaviour
{
    [Header("Individual Gems")]
    public IndividualGem[] individualGems;
    public GameObject[] individualGemsObjects;

    [Header("Gems Score")]
    public TextMeshProUGUI gemsScore;
    public int gemsAmount;
    public int gemValue;

    [Header("Gems Sprite SetUp")]
    public Sprite[] gemsSprites;
    private SpriteRenderer thisRend;
    private int desactiveIndex;

    private void Start()
    {
        thisRend = GetComponent<SpriteRenderer>();
        StartSetUp(0);
    }

    public void StartSetUp(int _gemsAmount)
    {
        desactiveIndex = 0;
        //Servira tanto para el Start como para Reset
        SetGemsAmount(_gemsAmount);
        SetGemsScore();
        //Coge el value inicial de la gema
        SetGemValue(1);
        //Coge el material inicial de las particulas
        //Coge el sprite inicial de la gema
    }

    public void SetGemsAmount(int _gemsAmount)
    {
        gemsAmount = _gemsAmount;
    }

    public int GetGemsAmount()
    {
        return gemsAmount;
    }

    public void SetGemsScore()
    {
        gemsScore.text = gemsAmount.ToString();
    }

    public void AddGem(int _value)
    {
        gemsAmount += _value;
        SetGemsScore();
    }

    public void SetGemValue(int _newValue)
    {
        CheckGemValue(_newValue);

        for (int i = 0; i < individualGems.Length; i++)
        {
            individualGems[i].SetGemValue(_newValue);
        }
    }

    public void CheckGemValue(int _value)
    {
        switch (_value)
        {
            case 1:
                SetGemSprites(0);

                break;

            case 2:
                SetGemSprites(1);
                break;

            case 3:
                SetGemSprites(2);
                break;
        }
    }

    public void SetGemSprites(int _index)
    {
        for (int i = 0; i < individualGems.Length; i++)
        {
            individualGems[i].SetGemSprites(_index, gemsSprites[_index]);
        }
    }

    public void CheckDesactive()
    {
        desactiveIndex++;

        if(desactiveIndex == individualGems.Length)
        {
            desactiveIndex = 0;
            this.gameObject.SetActive(false);
        }            
    }

    public void ActiveGems()
    {
        for (int i = 0; i < individualGemsObjects.Length; i++)
        {
            individualGemsObjects[i].SetActive(true);
        }
    }
}
