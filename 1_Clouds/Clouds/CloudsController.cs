using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsController : MonoBehaviour
{
    public GameObject[] clouds;
    public float time;
    private int contador;

    private void Start()
    {
        contador = 0;

        StartCoroutine(ActiveClouds());
    }

    public IEnumerator ActiveClouds()
    {
        while(contador < clouds.Length)
        {
            clouds[contador].SetActive(true);
            contador++;
            yield return new WaitForSeconds(time);
        }
    }
}
