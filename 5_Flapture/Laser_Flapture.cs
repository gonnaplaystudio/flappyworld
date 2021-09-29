using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Flapture : MonoBehaviour
{
    public Animator thisAnim;
    public string shootTrigger;
    public AudioSource laserSource;
    public AudioEvent laserEvent;
    public int startTime;
    private int actualTime;
    public bool VS_level;

    [Header("ArcadeSetUp")]
    public bool arcadeLevel;
    public int startArcTime;

    private void Start()
    {
        actualTime = 2;

        if (arcadeLevel)
            actualTime = startArcTime;

        StartCoroutine(ShootLaser());
    }

    private void OnEnable()
    {
        if (VS_level)
            StartCoroutine(ShootLaser());
    }

    public IEnumerator ShootLaser()
    {
        while (actualTime > 0)
        {
            actualTime--;

            yield return new WaitForSeconds(1f);
        }

        thisAnim.SetTrigger(shootTrigger);
    }

    public void SetUpTimeInAnim()
    {
        actualTime = startTime;

        StartCoroutine(ShootLaser());
    }

    public void ShootAudioInAnim()
    {
        laserEvent.Play(laserSource);
    }
}
