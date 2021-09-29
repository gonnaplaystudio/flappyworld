using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemsController : MonoBehaviour
{
    public TextMeshProUGUI gemsText;
    public int restAmount;//La cantidad que se va restando en la UI

    private int actualGems;
    private int resultGems;
    private bool active;

    private void Start()
    {
        actualGems = PlayerStats.Instance.GetGems();
        ShowGemsAmount();
    }

    public void SetGems(int _restGems)
    {
        if (active)
        {
            StopCoroutine(Contador());
            actualGems = resultGems;
            ShowGemsAmount();
        }            

        resultGems = actualGems - _restGems;
        StartCoroutine(Contador());
        //Se podria reproducir el sonido de restango gemas
    }

    public IEnumerator Contador()
    {
        SetActive(true);

        while (actualGems > resultGems)
        {
            actualGems -= restAmount;

            ShowGemsAmount();
            yield return new WaitForSeconds(.01f);
        }

        SetActive(false);
    }

    public void ShowGemsAmount()
    {
        gemsText.text = actualGems.ToString();
    }

    public void SetActive(bool _value)
    {
        active = _value;
    }
}
