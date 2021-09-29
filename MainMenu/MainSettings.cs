using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class MainSettings : MonoBehaviour
{
    [Header("Score Canvas")]
    public TextMeshProUGUI gemsText;
    public TextMeshProUGUI timer;
    public GameObject timerObject;
    public GameObject moreLifesButton;
    private Button lifesButton;
    public Image[] eneregyImages;
    public Color normalColor;
    public Color desactiveColor;

    [Header("Add Panel")]
    public GameObject addPanel;
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI timerOnAdd;
    public TutLanguage moreLifesLanguage;
    public bool inLevelsScene;

    [Header("Timer life")]
    public ulong lastTimeLifeLost;
    public float waitTime;

    private void Awake()
    {
        lifesButton = moreLifesButton.GetComponent<Button>();
    }

    private void Start()
    {
        StartSetUp();        
    }

    private void Update()
    {
        if (PlayerStats.Instance.GetLifes() == 0)
            CheckTimeLife();
    }

    public void StartSetUp()
    {
        lastTimeLifeLost = PlayerStats.Instance.GetLastDie();

        if (PlayerStats.Instance.GetLifes() <= 0 && inLevelsScene)
        {
            SetMoreLifesButton(true);
        }
        else
        {
            SetMoreLifesButton(false);
        }

        SetUpScorePanel();
    }

    public void CheckTimeLife()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - lastTimeLifeLost);
        ulong m = diff / TimeSpan.TicksPerMillisecond;
        float timeLfet = (float)(waitTime - m) / 1000f;

        string r = "";
        timeLfet -= ((int)timeLfet / 3600) * 3600;
        r += ((int)timeLfet / 60).ToString("00") + " : ";
        r += (timeLfet % 60).ToString("00");
        timer.text = r;

        if (addPanel.activeInHierarchy)
            timerOnAdd.text = r;

        if(timeLfet < 0)
        {
            PlayerStats.Instance.SetLifes(5);

            CheckEnergy(PlayerStats.Instance.GetLifes());

            DesactiveMoreLifes();             
        }
    }

    public void SetUpScorePanel()
    {
        SetUpGemsText();

        CheckEnergy(PlayerStats.Instance.GetLifes());

        if (PlayerStats.Instance.GetLifes() == 0)
            timerObject.SetActive(true);
    }

    public void DesactiveMoreLifes()
    {
        PlayerStats.Instance.SetLastDie(0);
        SetMoreLifesButton(false);
        timerObject.SetActive(false);
    }

    public void LifePanelButton()
    {
        SetAddPanel(true);
        SetUpAddPanel();
        UIAudio.Instance.ButtonFx();
    }

    public void CancelButton()
    {
        SetAddPanel(false);
        UIAudio.Instance.ButtonFx();
    }

    public void AddButton()//Volver al normal Cuando Se quite el anuncio Generico
    {
        if (PlayerStats.Instance.GetLifes() < 5)
        {
            //Se veria el anuncio
            //Se ponen las vidas al maximo
            PlayerStats.Instance.SetLifes(5);
            DesactiveMoreLifes();
            SetUpScorePanel();
            Data.Instance.SaveData();
            SetAddPanel(false);

            Analiticas.Instance.LifeAddInMenu();
            //UIAudio.Instance.ButtonFx();
        }
    }

    public void AddButtonInGenericAdd()//Quitar cuando hayan anuncios
    {
        if (PlayerStats.Instance.GetLifes() < 5)
        {
            //Se veria el anuncio
            //Se ponen las vidas al maximo
            PlayerStats.Instance.SetLifes(5);
            DesactiveMoreLifes();
            SetUpScorePanel();
            Data.Instance.SaveData();
            SetAddPanel(false);

            Analiticas.Instance.LifeAddInMenu();
            UIAudio.Instance.ButtonFx();
        }
    }

    public void SetAddPanel(bool _value)
    {
        addPanel.SetActive(_value);
    }

    public void SetUpAddPanel()
    {
        text1.text = moreLifesLanguage.GetTutLanguage(0);
        text2.text = moreLifesLanguage.GetTutLanguage(1);
    }

    public void CheckEnergy(int _energyLeft)
    {
        switch (_energyLeft)
        {
            case 5:
                ActiveAllEnergy();
                break;

            case 4:
                eneregyImages[0].color = desactiveColor;
                break;

            case 3:
                eneregyImages[0].color = desactiveColor;
                eneregyImages[1].color = desactiveColor;
                break;

            case 2:
                eneregyImages[0].color = desactiveColor;
                eneregyImages[1].color = desactiveColor;
                eneregyImages[2].color = desactiveColor;
                break;

            case 1:
                eneregyImages[0].color = desactiveColor;
                eneregyImages[1].color = desactiveColor;
                eneregyImages[2].color = desactiveColor;
                eneregyImages[3].color = desactiveColor;
                break;

            case 0:
                eneregyImages[0].color = desactiveColor;
                eneregyImages[1].color = desactiveColor;
                eneregyImages[2].color = desactiveColor;
                eneregyImages[3].color = desactiveColor;
                eneregyImages[4].color = desactiveColor;
                break;
        }
    }

    public void ActiveAllEnergy()
    {
        for (int i = 0; i < eneregyImages.Length; i++)
        {
            eneregyImages[i].color = normalColor;
        }
    }

    public void SetUpGemsText()
    {
        if(!inLevelsScene)
            gemsText.text = PlayerStats.Instance.GetGems().ToString();
    }

    public void SetMoreLifesButton(bool _value)
    {
        lifesButton.interactable = _value;
    }
}
