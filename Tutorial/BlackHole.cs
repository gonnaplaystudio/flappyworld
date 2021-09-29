using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackHole : MonoBehaviour
{
    [Header("Hole Exit SetUp")]
    public Animator thisAnim;
    private bool active;
    public float speed;
    public Transform playerTransform;
    public Transform holeCentre;
    public SpriteRenderer rend;
    public TutController tutController;
    public Animator scoreAnim;
    public string hideScoreTrigger;

    [Header("Audio SetUp")]
    public AudioSource thisAudioSource;
    public AudioEvent enterEvent;
    public AudioEvent exitEvent;
    public AudioEvent playerEnterEvent;

    [Header("Particles")]
    public ParticleSystem holeParticles;
    public ParticleSystem holeEndParticles;

    private void Update()
    {
        if (active)
        {
            playerTransform.position = Vector3.MoveTowards(playerTransform.position,
                holeCentre.position, speed * Time.deltaTime);
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //PlayerPrefs.SetString("FirstGame", "false");
            collision.transform.SetParent(transform.GetChild(2).transform);
            PlayerControllerTut.Instance.EnterTheHole();
            Invoke("ActiveHole", 2f);
            Invoke("LoadLevelsScene", 5f);

            active = true;

            playerEnterEvent.Play(thisAudioSource);

            PlayerStats.Instance.SetGems(tutController.GetGemsAmount());
            PlayerStats.Instance.SetTotalGems(tutController.GetGemsAmount());

            scoreAnim.SetTrigger(hideScoreTrigger);
        }
    }

    public void ActiveHole()
    {
        thisAnim.SetTrigger("Exit");
        rend.sortingOrder = 10;
        exitEvent.Play(thisAudioSource);
    }

    public void LoadLevelsScene()
    {
        SceneManager.LoadScene(3);
    }

    public void PlayEnterAudio()
    {
        enterEvent.Play(thisAudioSource);
    }

    public void PlayPlayerEnterAudio()
    {
        playerEnterEvent.Play(thisAudioSource);
    }

    public void PlayExitAudio()
    {
        exitEvent.Play(thisAudioSource);
    }
    public void ActiveParticles()
    {
        holeParticles.Play();
    }

    public void DesactiveParticles()
    {
        holeParticles.Stop();
    }

    public void ActiveEndParticles()
    {
        holeEndParticles.Play();
    }
}
