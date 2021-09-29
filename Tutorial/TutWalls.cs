using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutWalls : MonoBehaviour
{
    public GameObject gem;
    public Transform[] gemSpawns;
    public TutController tutController;
    public bool active;

    private void Awake()
    {
        active = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && active)
        {
            //Resta contador
            tutController.RestContador();

            if (!gem.activeInHierarchy)
            {
                gem.transform.position = gemSpawns[GetRandomNumber()].position;
                gem.SetActive(true);
            }
        }
    }

    public int GetRandomNumber()
    {
        int randomNumber = (int)Random.Range(0f, gemSpawns.Length);

        return randomNumber;
    }

    public void Desactive()
    {
        active = false;
    }

    public void Active()
    {
        active = true;
    }
}
