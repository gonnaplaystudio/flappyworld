using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geiser : MonoBehaviour
{
    public Animator thisAnim;
    public string activeTrigger;
    public ParticleSystem startParticles;
    public int startTime;
    private int actualTime;
    public bool VS_level;
    public bool bossLevel;

    private void Start()
    {
        if (bossLevel)
            return;

        actualTime = 4;
        StartCoroutine(ActiveGeiser());
    }

    private void OnEnable()
    {
        if(VS_level)
            StartCoroutine(ActiveGeiser());
    }

    public IEnumerator ActiveGeiser()
    {
        while(actualTime > 0)
        {
            actualTime--;

            if (actualTime == 3)
                startParticles.Play();



            yield return new WaitForSeconds(1f);
        }

        thisAnim.SetTrigger(activeTrigger);
    }

    public void SetActualTimeInAnim()
    {
        if (bossLevel)
            return;

        actualTime = startTime;
        StartCoroutine(ActiveGeiser());
    }
}
