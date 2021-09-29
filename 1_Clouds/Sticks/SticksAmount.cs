using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SticksAmount : MonoBehaviour
{
    [Header("Sticks Audio")]
    public AudioSource sticksAudioSource;
    public AudioEvent sticksEvent;

    [Header("Sticks SetUp")]
    public int sticksAmount;
    private GameObject[] sticks;
    public List<int> randomNumbers;
    public int easySticks;
    public int normalSticks;
    public int hardiSticks;

    [Header("Boss")]
    public bool bossLevel;

    private void Start()
    {
        SetSticksAmount(PlayerStats.Instance.GetDifficulty());

        sticks = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            sticks[i] = transform.GetChild(i).gameObject;
        }

        DesactiveAllSticks();
    }

    //AuxController los utilizara para aumentar o resetear los sticks que aparecen
    public void SetSticksAmount(string _difficulty)
    {
        if (bossLevel)
            return;

        switch (_difficulty)
        {
            case "Easy":
                sticksAmount = easySticks;
                break;

            case "Normal":
                sticksAmount = normalSticks;
                break;

            case "Hard":
                sticksAmount = hardiSticks;
                break;
        }
    }

    public void SetSticksAmountBoss(int _sticksAmount)
    {
        sticksAmount = _sticksAmount;
    }

    //Funcion en la animacion de Salida
    public void DesactiveAllSticks()
    {
        foreach (GameObject stick in sticks)
        {
            stick.SetActive(false);
        }

        randomNumbers.Clear();
    }

    //Funcion en la animacion de Entrada
    public void SetRandomSticks()
    {
        for (int i = 0; i < sticksAmount; i++)
        {
            sticks[GetRandomNumber()].SetActive(true);
        }

        sticksEvent.Play(sticksAudioSource);
    }

    public int GetRandomNumber()
    {
        int randomNumber = (int)Random.Range(0f, sticks.Length);

        while (randomNumbers.Contains(randomNumber))
        {
            randomNumber = (int)Random.Range(0f, sticks.Length);
        }

        randomNumbers.Add(randomNumber);

        return randomNumber;
    }
}
