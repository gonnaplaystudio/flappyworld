using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Time_Trial : MonoBehaviour
{
    [Header("Black Hole Attributes")]
    public Animator blackHoleAnim;
    public string endHoleTrigger;
    public string desactiveHole;
    private bool activeHole;

    [Header("Audio Set Up")]
    public AudioEvent counterEvent;
    public AudioEvent finalCounterEvent;
    public AudioEvent clockEvent;
    public AudioSource counterSource;
    public AudioSource clockSource;

    public int startTime;
    public int time;

    public TextMeshProUGUI timerText;

    public Gems_TT_Controller gems_TT_Controller;
    public Time_Container time_Container;

    private void Awake()
    {
        SetStartTime();
        SetUpTimerText();
    }

    public IEnumerator TimeTrial()
    {
        while (time > 0)
        {
            time--;

            SetUpTimerText();

            CheckTimer(time);

            yield return new WaitForSeconds(1f);
        }
    }

    public void StartTimer()
    {
        StartCoroutine(TimeTrial());
    }

    public void StopTime()
    {
        //StopCoroutine(TimeTrial());
        time = 0;
    }

    public void SetUpTimerText()
    {
        timerText.text = time.ToString();
    }

    public void SetStartTime()
    {
        time = startTime;
    }

    public void CheckTimer(int _timer)
    {
        switch (_timer)
        {
            case 3:
                //Sonido cuenta atras
                PlayCounterAudio();
                break;

            case 2:
                PlayCounterAudio();
                break;

            case 1:
                PlayCounterAudio();
                break;

            case 0:
                //Campanita de finalizar
                PlayFinalCounterAudio();
                //Se desactivan las gemas
                gems_TT_Controller.DesactiveGems();
                //Se desactivan los Time_TT
                time_Container.SetUpTime(false);
                //Se activa el black hole
                SetActiveHole(true);
                SetUpEndHole();
                break;
        }
    }

    public void AddTime(int _time)
    {
        time += _time;
        SetUpTimerText();
        PlayClockAudio();
    }

    public void SetUpEndHole()
    {
        blackHoleAnim.SetTrigger(endHoleTrigger);
    }

    public void DesactiveEndHole()
    {
        blackHoleAnim.SetTrigger(desactiveHole);
    }

    public void SetActiveHole(bool _value)
    {
        activeHole = _value;
    }

    public bool GetActive()
    {
        return activeHole;
    }

    public void PlayClockAudio()
    {
        clockEvent.Play(clockSource);
    }

    public void PlayCounterAudio()
    {
        counterEvent.Play(counterSource);
    }

    public void PlayFinalCounterAudio()
    {
        finalCounterEvent.Play(counterSource);
    }
}
