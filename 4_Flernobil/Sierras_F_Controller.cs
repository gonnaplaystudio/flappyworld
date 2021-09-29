using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sierras_F_Controller : MonoBehaviour
{
    public GameObject[] sierras_L;
    public GameObject[] sierras_R;

    public float[] spawnTimeRate_min;
    public float[] spawnTimeRate_max;
    public float spawnRate_min;
    public float spanwRate_max;


    [Header("TEST MODE")]
    public int difficultyIndex;

    private void Start()
    {
        CheckSpawnTimeRate(PlayerStats.Instance.GetDifficulty());

        StartCoroutine(SpawnSierras_L());
        StartCoroutine(SpawnSierras_R());
    }

    public IEnumerator SpawnSierras_L()
    {
        while (true)
        {
            foreach(GameObject _sierras in sierras_L)
            {
                if (!_sierras.activeInHierarchy)
                {
                    _sierras.SetActive(true);
                    break;
                }                    
            }

            yield return new WaitForSeconds(GetRandomNumber());
        }
    }

    public IEnumerator SpawnSierras_R()
    {
        while (true)
        {
            foreach (GameObject _sierras in sierras_R)
            {
                if (!_sierras.activeInHierarchy)
                {
                    _sierras.SetActive(true);
                    break;
                }
            }

            yield return new WaitForSeconds(GetRandomNumber());
        }
    }

    public void CheckSpawnTimeRate(string _difficulty)
    {
        switch (_difficulty)
        {
            case "Easy":
                spawnRate_min = spawnTimeRate_min[0];
                spanwRate_max = spawnTimeRate_max[0];
                break;

            case "Normal":
                spawnRate_min = spawnTimeRate_min[1];
                spanwRate_max = spawnTimeRate_max[1];
                break;

            case "Hard":
                spawnRate_min = spawnTimeRate_min[2];
                spanwRate_max = spawnTimeRate_max[2];
                break;

            default:
                spawnRate_min = spawnTimeRate_min[difficultyIndex];
                spanwRate_max = spawnTimeRate_max[difficultyIndex];
                break;
        }
    }

    public float GetRandomNumber()
    {
        float random = Random.Range(spawnRate_min, spanwRate_max);

        return random;
    }
}
