using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time_Container : MonoBehaviour
{
    public GameObject[] times;

    private void Awake()
    {
        SetTimers();
    }

    //Coge los hijos del objeto
    public void SetTimers()
    {
        times = new GameObject[transform.childCount];

        for (int i = 0; i < times.Length; i++)
        {
            times[i] = transform.GetChild(i).gameObject;
        }
    }

    //Activa o desactiva los hijos
    public void SetUpTime(bool _value)
    {
        for (int i = 0; i < times.Length; i++)
        {
            times[i].SetActive(_value);
        }
    }
}
