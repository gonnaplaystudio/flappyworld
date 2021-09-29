using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contenedor_Sierras_TT : MonoBehaviour
{
    public GameObject[] sierrasEstaticas_I;
    public GameObject[] sierrasEstaticas_D;
    public GameObject[] sierrasMovimiento;
    public Sierra[] sierrasScript;//Para cambiar de direccion
    public Gems_TT_Controller gems_TT_Controller;

    private void Start()
    {
        CheckDifficulty(gems_TT_Controller.actualDifficulty);
    }

    public void CheckDifficulty(string _difficulty)
    {
        switch (_difficulty)
        {
            case "Easy":
                SetUpEasy(GetRandomNumber());
                break;

            case "Normal":
                SetUpNormal(GetRandomNumber());
                break;

            case "Hard":
                SetUpHard(GetRandomNumber());
                break;

            default:
                SetUpEasy(GetRandomNumber());
                break;
        }
    }

    public void SetUpEasy(int _random)
    {
        switch (_random)
        {
            case 0:
                sierrasEstaticas_I[0].SetActive(true);
                sierrasEstaticas_I[6].SetActive(true);
                sierrasEstaticas_I[2].SetActive(true);
                sierrasEstaticas_D[5].SetActive(true);
                sierrasEstaticas_D[1].SetActive(true);
                sierrasEstaticas_D[7].SetActive(true);
                break;

            case 1:
                sierrasEstaticas_I[5].SetActive(true);
                sierrasEstaticas_I[1].SetActive(true);
                sierrasEstaticas_I[7].SetActive(true);
                sierrasEstaticas_D[0].SetActive(true);
                sierrasEstaticas_D[6].SetActive(true);
                sierrasEstaticas_D[2].SetActive(true);
                break;
        }
    }

    public void SetUpNormal(int _random)
    {
        switch (_random)
        {
            case 0:
                sierrasEstaticas_I[5].SetActive(true);
                sierrasEstaticas_I[1].SetActive(true);
                sierrasEstaticas_I[6].SetActive(true);
                sierrasEstaticas_I[2].SetActive(true);
                sierrasEstaticas_D[0].SetActive(true);
                sierrasEstaticas_D[6].SetActive(true);
                sierrasEstaticas_D[1].SetActive(true);
                sierrasEstaticas_D[7].SetActive(true);
                break;

            case 1:
                sierrasEstaticas_I[0].SetActive(true);
                sierrasEstaticas_I[1].SetActive(true);
                sierrasEstaticas_I[2].SetActive(true);
                sierrasEstaticas_I[6].SetActive(true);
                sierrasEstaticas_D[5].SetActive(true);
                sierrasEstaticas_D[6].SetActive(true);
                sierrasEstaticas_D[7].SetActive(true);
                sierrasEstaticas_D[1].SetActive(true);
                break;
        }
    }

    public void SetUpHard(int _random)
    {
        switch (_random)
        {
            case 0:
                sierrasEstaticas_I[0].SetActive(true);
                sierrasEstaticas_I[3].SetActive(true);
                sierrasEstaticas_I[4].SetActive(true);
                sierrasEstaticas_I[2].SetActive(true);
                sierrasEstaticas_D[5].SetActive(true);
                sierrasEstaticas_D[3].SetActive(true);
                sierrasEstaticas_D[4].SetActive(true);
                sierrasEstaticas_D[7].SetActive(true);
                break;

            case 1:
                sierrasEstaticas_I[0].SetActive(true);
                sierrasEstaticas_I[1].SetActive(true);
                sierrasEstaticas_I[2].SetActive(true);
                sierrasEstaticas_I[5].SetActive(true);
                sierrasEstaticas_I[7].SetActive(true);
                sierrasEstaticas_D[0].SetActive(true);
                sierrasEstaticas_D[5].SetActive(true);
                sierrasEstaticas_D[6].SetActive(true);
                sierrasEstaticas_D[7].SetActive(true);
                sierrasEstaticas_D[2].SetActive(true);
                break;
        }
    }

    public int GetRandomNumber()
    {
        int randomNumber = (int)Random.Range(0f, 2);
        return randomNumber;
    }

}
