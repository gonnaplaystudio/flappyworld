using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud_Map_Controller : MonoBehaviour
{
    public GameObject[] obstaculos;
    public Gems_TT_Controller gem_TT_Controller;

    private void Start()
    {
        ActiveObstaculos(gem_TT_Controller.actualDifficulty);
    }

    public void ActiveObstaculos(string _difficulty)
    {
        switch (_difficulty)
        {
            case "Easy":
                obstaculos[0].SetActive(true);
                break;

            case "Normal":
                obstaculos[1].SetActive(true);
                break;

            case "Hard":
                obstaculos[2].SetActive(true);
                break;

            default:
                obstaculos[0].SetActive(true);
                break;

        }
    }
}
