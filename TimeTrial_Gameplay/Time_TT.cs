using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time_TT : MonoBehaviour
{
    public Time_Trial timer_Trial;
    public int timeValue;
    public Animator thisAnim;
    public string takeTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //audioLevels.TimerAudio();

            timer_Trial.AddTime(timeValue);
            thisAnim.SetTrigger(takeTrigger);

            Invoke("Desactive", .5f);
        }
    }

    public void Desactive()
    {
        this.transform.parent.gameObject.SetActive(false);
    }
}
