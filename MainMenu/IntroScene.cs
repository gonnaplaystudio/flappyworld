using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class IntroScene : MonoBehaviour
{
    [Header("CHAPTER")]
    public string chapterName;

    [Header("Fade SetUp")]
    public Animator fadeAnim;
    public string fadeTrigger;
    public float waitTimeFade;
    public Button introButton;
    public GameObject flappy;

    [Header("Start SetUp")]
    public Language languageArchive;
    public TextMeshProUGUI startText;
    public Animator startTextAnim;
    public AudioSource startSource;
    public AudioEvent startEvent;

    [Header("LanguagePanel")]
    public GameObject languagePanel;

    [Header("Intro SetUp")]
    public Animator introAnim;
    public string introTrigger;
    public AudioSource sountrackSource;
    public AudioSource introFadeSource;
    public AudioEvent introFadeEvent;
    public AudioEvent sountrackEvent;

    public bool betaActiva = true;
    public GameObject panelBetaCerrada;
    public RemoteConfig remoteConf;

    [Header("RESETEO")]
    public Flappy selectedFlappy;
    public int actualGemsAmount;
    public int unlockFlappyIndex;//Hasta donde desbloqueas los Flappys
    public int newPackFlappyUnlock;//Flappy Desbloqueado de la nueva temporada

    private void Awake()
    {
        Invoke("SetInteractableButton", 1.5f);
        Invoke("ChargeIntro", .5f);
    }

    private void Start()
    {
        //Data.Instance.CreateData();

        if (Convert.ToBoolean(PlayerPrefs.GetString("FirstGame", "true")))
        {
            Data.Instance.CreateData();
            languagePanel.SetActive(true);
            PlayerPrefs.SetString("ChapterName", chapterName);
        }
        else
        {
            Invoke("ActiveFlappy", 1f);
        }

        Data.Instance.LoadData();

        if(PlayerPrefs.GetString("ChapterName","Capitulo1") != chapterName)
        {
            //selectedFlappy = PlayerStats.Instance.GetFlappy();
            //actualGemsAmount = PlayerStats.Instance.GetGems();

            Data.Instance.CreateData();

            PlayerStats.Instance.ResetChapter();
            PlayerPrefs.SetString("ChapterName", chapterName);

            PlayerStats.Instance.UnlockFlappysPack(unlockFlappyIndex);
            //PlayerStats.Instance.SetFlappyShop(newPackFlappyUnlock, true);

            PlayerStats.Instance.SetFlappy(selectedFlappy);
            PlayerStats.Instance.SetGems(actualGemsAmount);

            Data.Instance.SaveData();

            Debug.Log("RESETEO CAPITULO");
        }

        //PlayerStats.Instance.SetGems(2500);
        //PlayerStats.Instance.AddAdventurePoints(10000);
        //PlayerStats.Instance.SetLifes(1);


    }

    public void ActiveFlappy()
    {
        flappy.SetActive(true);
    }

    public void ChargeIntro()
    {
        if (!Convert.ToBoolean(PlayerPrefs.GetString("FirstGame", "true")))
            SetUpIntro();

        //SetStartText();
    }

    public void Pressed()
    {
        sountrackSource.Stop();

        introButton.interactable = false;
        PlayerStats.Instance.SetFadeFromLevels(true);
        fadeAnim.SetTrigger(fadeTrigger);
        //Animacion de Press Start
        startTextAnim.SetTrigger("Start");
        //Sonido de Press Start
        startEvent.Play(startSource);

        if(Convert.ToBoolean(PlayerPrefs.GetString("FirstGame", "true")))
        {
            Invoke("LoadTuTScene", waitTimeFade);
        }
        else
        {
            Invoke("LoadLevelScene", waitTimeFade);
        }
    }

    public void LoadLevelScene()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadTuTScene()
    {
        SceneManager.LoadScene(2);
    }

    public void SetStartText()
    {
        startText.text = languageArchive.IntroStart();
    }

    public void SetUpIntro()
    {
        SetStartText();

        introAnim.SetTrigger(introTrigger);
        introFadeEvent.Play(introFadeSource);
        sountrackEvent.Play(sountrackSource);
    }

    public void DesactiveLanguagePanel()
    {
        languagePanel.SetActive(false);
        Invoke("ActiveFlappy", 1f);
    }

    public void SetInteractableButton()
    {
        introButton.interactable = true;
    }



    //Para pruebas
    public void ResetButton()
    {
        PlayerPrefs.DeleteAll();

        Data.Instance.CreateData();
    }
}
