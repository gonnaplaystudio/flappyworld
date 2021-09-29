using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TutController : MonoBehaviour
{
    [Header("Score SetUp")]
    public TextMeshProUGUI gemsText;
    public TextMeshProUGUI contadorText;
    public TextMeshProUGUI lifeText;
    public int contadorAmount;
    public int lifesAmount;
    public int gemsAmount;

    [Header("Exit SetUp")]
    public Animator exitHole;
    public string endLevelString;
    public string playerDie;
    public TutWalls[] tutWalls;
    private bool activeHole;

    [Header("Player Die")]
    public StartGame startGame;
    public Transform playerTransform;
    public Transform startPlayerPos;
    public Animator playerAnim;
    public string playerRessurectTrigger;
    public Collider2D[] sticksCollider;
    public PlayerControllerTut playerController;

    [Header("Tutorial SetUp")]
    public Tutorial tutorial;
    public Animator sticksAnim;
    public string hideSticksTrigger;
    public bool firstDie;
    public bool firstGem;

    [Header("Audio")]
    public AudioSource sountrackSource;
    public AudioEvent sountrackEvent;
    public AudioEvent openingEvent;
    public AudioSource wallsSource;
    public AudioEvent counterEvent;

    private void Awake()
    {
        firstDie = true;
        firstGem = true;

        contadorAmount = 10;
        gemsAmount = 0;
        lifesAmount = 3;

        SetUpText();
    }

    private void Start()
    {
        Invoke("OpenAudio", .5f);
        Invoke("SountrackAudio", 2.5f);
    }

    public void OpenAudio()
    {
        sountrackSource.loop = false;
        openingEvent.Play(sountrackSource);
    }

    public void SountrackAudio()
    {
        sountrackSource.loop = true;
        sountrackEvent.Play(sountrackSource);
    }

    public void RestContador()
    {
        if (contadorAmount == 0)
            return;

        contadorAmount--;

        SetUpContadorText();

        counterEvent.Play(wallsSource);

        if(contadorAmount == 0)
        {
            activeHole = true;
            Invoke("EndTut", 1f);
        }
    }

    public void EndTut()
    {
        if (activeHole)
        {
            exitHole.SetTrigger(endLevelString);
            sticksAnim.SetTrigger(hideSticksTrigger);
            tutWalls[0].Desactive();
            tutWalls[1].Desactive();
        }        
    }

    public void PlayerDead()
    {
        lifesAmount--;
        contadorAmount = 10;
        gemsAmount = 0;   
        
        activeHole = false;

        int lostLifes = PlayerPrefs.GetInt("Lost_Lifes", 0);
        lostLifes++;
        PlayerPrefs.SetInt("Lost_Lifes", lostLifes);

        if (firstDie)
        {
            //Activa el canvas de muerte
            firstDie = false;
            tutorial.SetAnim(3);
            Invoke("FirstDead", .5f);
            SetUpText();
            return;
        }


        if (lifesAmount == 0)
        {
            lifesAmount = 3;
            gemsAmount = 0;
            tutorial.SetAnim(3);
            Invoke("FirstDead", .5f);
            SetUpText();
            return;
        }


        Invoke("Resurect", .5f);

        SetUpText();
    }

    public void Resurect()
    {
        playerTransform.position = startPlayerPos.position;
        playerAnim.enabled = true;
        playerAnim.SetTrigger(playerRessurectTrigger);
        startGame.enabled = true;
        startGame.StartTheGame();
    }

    public void FirstDead()
    {
        playerTransform.position = startPlayerPos.position;
        playerAnim.enabled = true;
        playerAnim.SetTrigger(playerRessurectTrigger);
        startGame.enabled = true;
        startGame.StartTheGame();
        startGame.enabled = false;
    }

    public void AddGem()
    {
        if (firstGem)
        {
            firstGem = false;
            Invoke("StopPlayer", 1f);
            //Animacion de primera gema
            tutorial.SetAnim(1);
        }

        gemsAmount++;
        SetUpGemsText();
    }

    public void StopPlayer()
    {
        playerController.StopPlayer();
    }

    public void SetUpText()
    {
        SetUpGemsText();
        SetUpLifesText();
        SetUpContadorText();
    }

    public void SetUpGemsText()
    {
        gemsText.text = gemsAmount.ToString();      
        
    }

    public void SetUpContadorText()
    {
        contadorText.text = contadorAmount.ToString();
    }

    public void SetUpLifesText()
    {
        lifeText.text = lifesAmount.ToString();
    }

    public void DesactiveSticks()
    {
        sticksAnim.SetTrigger(hideSticksTrigger);

        for (int i = 0; i < sticksCollider.Length; i++)
        {
            sticksCollider[i].enabled = false;
        }
    }

    public int GetGemsAmount()
    {
        return gemsAmount;
    }
}
