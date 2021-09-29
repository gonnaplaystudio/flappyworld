using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class Reset_TT : MonoBehaviour
{
    [Header("Player Attributes")]
    public Transform playerTransform;
    public Animator playerAnim;
    public string playerRessurectTrigger;
    private Vector3 startPlayerPosition;
    public CinemachineVirtualCamera cinemaCam;

    [Header("Reset Objects")]
    public StartGame_TT startGame_TT;
    public Gems_TT_Controller gems_TT_Controller;
    public Time_Trial time_Trial;

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
        startPlayerPosition = playerTransform.position;

        lifes = PlayerStats.Instance.GetHearts();
        SetUpLifesText();
    }

    public IEnumerator SlowlyDeath()
    {
        Time.timeScale = .3f;

        while (Time.timeScale < 1)
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

        //Resta vidas
        lifes--;
        SetUpLifesText();

        //Comprueba si el jugador tiene un corazon extra
        if (PlayerStats.Instance.GetExtraHeart())
        {
            PlayerStats.Instance.SetExtraHeart(false);
            PlayerStats.Instance.SetHearts(3);
            Data.Instance.SaveData();
        }

        //Veces que ha muerto
        int lifesLost = PlayerPrefs.GetInt("Lost_Lifes", 0);
        lifesLost++;
        PlayerPrefs.SetInt("Lost_Lifes", lifesLost);

        //Desactiva la gema si hace falta

        //Desactiva el cofre si esta activado
        if (time_Trial.GetActive())
        {
            time_Trial.DesactiveEndHole();
            time_Trial.SetActiveHole(false);
        }

        //Para el Time_Trial
        time_Trial.StopTime();

        //Se comprueba si quedan vidas
        if (lifes == 0)
        {
            //Quita las gemas obtenidas
            

            //Si no quedan vidas activa el fadeInLevels y game over canvas
            Invoke("GameOver", 2f);
            audioLevels.StopSountrack();

            StartCoroutine(SlowlyDeath());

            PauseControll.Instance.DesactiveScore();
        }
        else
        {
            //Reduce la gemas obtenidas a la mitad
            //int _gemsAmount = gems_TT_Controller.GetGemsAmount() / 2;

            //Redice las gemas a 0
            gems_TT_Controller.SetGemsAmount(0);

            gems_TT_Controller.SetUpGemsText();

            //Si quedan vidas resucita
            Invoke("Ressurect", .5f);
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
        startGame_TT.enabled = true;
        startGame_TT.StartTheGame();
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

    public void SetCinemCam(bool _value)
    {
        if (cinemaCam != null)
            cinemaCam.enabled = _value;
    }
}
