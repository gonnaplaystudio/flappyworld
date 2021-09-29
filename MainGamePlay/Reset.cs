using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class Reset : MonoBehaviour
{
    [Header("Player Attributes")]
    public Transform playerTransform;
    public Animator playerAnim;
    public string playerRessurectTrigger;
    private Vector3 startPlayerPosition;
    public CinemachineVirtualCamera cinemaCam;

    [Header("Aux Controllers")]
    public AuxStickController auxSticks;//Para el nivel de las nubes
    public ParallaxController parallaxController;//Para los niveles largos con parallax

    [Header("Reset Objects")]
    public Difficulty difficulty;
    public StartGame startGame;
    public Gem gem;
    public GameObject gemObject;

    [Header("Lifes SetUp")]
    public TextMeshProUGUI lifesText;
    public int lifes;

    [Header("Game Over")]
    public Animator fadeInLevelsAnim;
    public string exitFadeTrigger;
    public GameObject gameOverCanvas;
    public AudioInLevels audioLevels;

    private void Start()
    {
        lifes = PlayerStats.Instance.GetHearts();
        startPlayerPosition = playerTransform.position;
        SetUpLifesText();

    }

    public IEnumerator SlowlyDeath()
    {
        Time.timeScale = .3f;

        while(Time.timeScale < 1)
        {
            Time.timeScale += .05f;

            yield return new WaitForSeconds(.1f);
        }

        Time.timeScale = 1;
    }

    public void CheckPlayerLifes()
    {
        //Lleva al jugador a la posicion inicial
        SetCinemCam(false);

        //Hace un reseteo auxiliar si hace falta
        AuxStickReset();

        //Comprueba si el jugador tiene un corazon extra
        if (PlayerStats.Instance.GetExtraHeart())
        {
            PlayerStats.Instance.SetExtraHeart(false);
            PlayerStats.Instance.SetHearts(3);
            Data.Instance.SaveData();
        }

        //Resta vidas
        lifes--;
        SetUpLifesText();

        //Veces que ha muerto
        int lifesLost = PlayerPrefs.GetInt("Lost_Lifes", 0);
        lifesLost++;
        PlayerPrefs.SetInt("Lost_Lifes", lifesLost);

        //Desactiva la gema si hace falta

        //Desactiva el cofre si esta activado
        if (difficulty.GetActive())
        {
            difficulty.DesactiveEndHole();
            difficulty.SetActiveHole(false);
        }

        //Comprueba si hay parallax
        CheckParallax();

        //Se comprueba si quedan vidas
        if(lifes == 0)
        {
            //Quita las gemas obtenidas
            gem.StartSetUp(0);
            gemObject.SetActive(false);
            //Si no quedan vidas activa el fadeInLevels y game over canvas
            Invoke("GameOver", 2f);
            audioLevels.StopSountrack();

            StartCoroutine(SlowlyDeath());

            PauseControll.Instance.DesactiveScore();
        }
        else
        {
            //Reduce la gemas obtenidas a la mitad
            //int _gemsAmount = gem.GetGemsAmount() / 2;

            //Reduce las gemas a 0
            gem.StartSetUp(0);
            gemObject.SetActive(false);

            //Si quedan vidas resucita
            Invoke("Ressurect", .5f);

            //Para los niveles con Parallax
            Invoke("SetStartParallaxPos", 0.5f);
        }

        Data.Instance.SaveData();

    }

    public void SetUpLifesText()
    {
        lifesText.text = lifes.ToString();
    }

    public void Ressurect()
    {
        playerTransform.position = startPlayerPosition;
        SetCinemCam(true);

        playerAnim.enabled = true;
        playerAnim.SetTrigger(playerRessurectTrigger);
        startGame.enabled = true;
        startGame.StartTheGame();

        difficulty.SetStartCounter();
    }

    public void GameOver()
    {
        Invoke("GameOverAudio", 1f);
        fadeInLevelsAnim.SetTrigger(exitFadeTrigger);
        Invoke("ActiveGameOverCanvas", 2.2f);
        //Para los sonidos que hayan en la escena(boss, lasers, etc)
    }

    public void GameOverAudio()
    {
        audioLevels.GameOverAudio();
    }

    public void ActiveGameOverCanvas()
    {
        gameOverCanvas.SetActive(true);
    }

    public void AuxStickReset()
    {
        if (auxSticks == null)
            return;

        auxSticks.AuxReset();
    }

    public void CheckParallax()
    {
        if(parallaxController != null)
        {
            parallaxController.SetUpParalaxes(false);
        }
    }

    public void SetStartParallaxPos()
    {
        if(parallaxController != null)
            parallaxController.SetStartPosition();
    }

    public void SetCinemCam(bool _value)
    {
        if (cinemaCam != null)
            cinemaCam.enabled = _value;
    }
}
