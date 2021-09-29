using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;

public class GameOver : MonoBehaviour
{
    public static GameOver Instance;

    //Encargado del GameOverCanvas y los anuncios
    [Header("More Lifes Panel SetUp")]
    public TextMeshProUGUI lifesText;
    public TextMeshProUGUI addText;
    public TutLanguage moreLifesLanguage;

    [Header("Lose Panel SetUp")]
    public TextMeshProUGUI loseText;
    public TextMeshProUGUI loseText2;
    public TextMeshProUGUI textButton;
    public TutLanguage losePanelLanguage;

    [Header("Anim SetUp")]
    public Animator gameOverCanvasAnim;
    public string exitTrigger;
    public int thisSceneIndex;

    [Header("Energy Set Up")]
    public Image[] energy;
    public Color normalColor;
    public Color disableColor;
    public Image vanishEnergy;
    public bool vanish;
    public AudioEvent energyVanishEvent;
    public AudioSource thisSource;
    private bool retryValue;

    [Header("Buttons Set Up")]
    public GameObject resetButton;
    public GameObject addButton;
    public GameObject resetText;
    public GameObject addTextObject;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Warning: More than one Game Over Instance");
            return;
        }

        Instance = this;

        SetUpGameOverPanel();

        Analiticas.Instance.LevelLoseAnalitycs(PlayerStats.Instance.GetDifficulty());
    }

    private void Start()
    {
        SetUpText();
        SetUpEnergy(PlayerStats.Instance.GetLifes());
        CheckPause();
    }

    private void Update()
    {
        if (vanish)
            vanishEnergy.color = new Color(vanishEnergy.color.r, vanishEnergy.color.g,
                vanishEnergy.color.b, vanishEnergy.color.a - Time.deltaTime);
    }

    public void BackToTheMenu()
    {
        SceneManager.LoadScene(3);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(thisSceneIndex);
    }

    public void MenuButton()
    {
        SetRetryButton(true);

        PlayerStats.Instance.SetFadeFromLevels(true);
        DesactiveGameOverCanvas();
        Invoke("BackToTheMenu", 1f);
    }

    //BOTON DE VOLVER A INTENTARLO CUANDO AUN QUEDA ENERGIA
    public void ResetButton()
    {
        if (retryValue)
            return;

        SetRetryButton(true);

        Invoke("DesactiveGameOverCanvas", 1f);

        PlayerStats.Instance.RestLifes();
        //enseña que se quita energia
        //SetUpEnergy(PlayerStats.Instance.GetLifes());
        VanishEnergy(PlayerStats.Instance.GetLifes());

        if (PlayerStats.Instance.GetLifes() <= 0)
        {
            PlayerStats.Instance.SetLastDie((ulong)DateTime.Now.Ticks);
        }

        Data.Instance.SaveData();
        Invoke("ResetScene", 2f);
    }

    public void SetRetryButton(bool _value)
    {
        retryValue = _value;
    }

    public void AddButton_2()
    {
        AnuncioController.Instance.RewardVideo("lifesGamePlay");
    }

    public void AdButton()//Esto va en el boton de anuncio cuando se quite el generico
    {
        if (retryValue)
            return;

        SetRetryButton(true);
        //Para el boton del anuncio


        //Desactiva el canvas
        DesactiveGameOverCanvas();

        //Anuncio

        //Lo que iria si se viera el anuncio
        PlayerStats.Instance.SetLifes(5);
        PlayerStats.Instance.SetLastDie(0);
        Data.Instance.SaveData();
        //Tras el anuncio
        Invoke("ResetScene", 1f);

        //QUITAR UNA ENERGIA
        PlayerStats.Instance.RestLifes();

        Analiticas.Instance.LifesAddInLevels();

        //AnuncioController.Instance.GamePlayVideo();
    }

    public void AdButtonInGenericAdd()//Esto va en el anuncioGenerico
    {
        //Para el boton del anuncio

        //Desactiva el canvas
        DesactiveGameOverCanvas();

        //Anuncio

        //Lo que iria si se viera el anuncio
        PlayerStats.Instance.SetLifes(5);
        PlayerStats.Instance.SetLastDie(0);
        Data.Instance.SaveData();
        //Tras el anuncio
        Invoke("ResetScene", 1f);

        

        Analiticas.Instance.LifesAddInLevels();
    }

    public void DesactiveGameOverCanvas()
    {
        gameOverCanvasAnim.SetTrigger(exitTrigger);
    }

    public void SetUpText()
    {
        lifesText.text = moreLifesLanguage.GetTutLanguage(0);
        addText.text = moreLifesLanguage.GetTutLanguage(1);

        loseText.text = losePanelLanguage.GetTutLanguage(0);
        loseText2.text = losePanelLanguage.GetTutLanguage(1);
        textButton.text = losePanelLanguage.GetTutLanguage(2);
    }

    public void SetUpGameOverPanel()
    {
        if (PlayerStats.Instance.GetLifes() > 0)
        {
            resetButton.SetActive(true);
            resetText.SetActive(true);
        }
        else
        {
            addButton.SetActive(true);
            addTextObject.SetActive(true);
        }
    }

    public void SetUpEnergy(int _energyValue)
    {
        switch (_energyValue)
        {
            case 5:
                
                break;

            case 4:
                energy[0].color = disableColor;
                break;

            case 3:
                energy[0].color = disableColor;
                energy[1].color = disableColor;
                break;

            case 2:
                energy[0].color = disableColor;
                energy[1].color = disableColor;
                energy[2].color = disableColor;
                break;

            case 1:
                energy[0].color = disableColor;
                energy[1].color = disableColor;
                energy[2].color = disableColor;
                energy[3].color = disableColor;
                break;

            case 0:
                energy[0].color = disableColor;
                energy[1].color = disableColor;
                energy[2].color = disableColor;
                energy[3].color = disableColor;
                energy[4].color = disableColor;
                break;
        }
    }

    public void VanishEnergy(int _energyValue)
    {
        switch (_energyValue)
        {

            case 4:                
                SetVanishEnergy(energy[0]);
                break;

            case 3:
                SetVanishEnergy(energy[1]);
                break;

            case 2:
                SetVanishEnergy(energy[2]);
                break;

            case 1:
                SetVanishEnergy(energy[3]);
                break;

            case 0:
                SetVanishEnergy(energy[4]);
                break;
        }
    }

    public void SetVanishEnergy(Image _vanishImage)
    {
        vanishEnergy = _vanishImage;
        vanish = true;
        //Sonido de Energia consumiendose

        energyVanishEvent.Play(thisSource);
    }

    public void CheckPause()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        PauseControll.Instance.enabled = false;
    }
}
