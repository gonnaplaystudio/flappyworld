using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelsController : MonoBehaviour
{
    public static LevelsController Instance;

    [Header("MapsSetUp")]
    public TextMeshProUGUI mapText;
    public Image mapsImage;
    public MapScriptable[] maps;
    public GameObject[] buttonsContainers;//Coontenedor de los botones de los mapas
    public Animator mapAnim;
    public string changeTrigger;
    private int mapsIndex;//Valor a guardar

    [Header("Actual Flappy")]
    public Image actualFlappy;

    [Header("Fade")]
    public FadeController fadeController;
    public AudioSource fadeSource;
    public AudioEvent fadeEvent;

    [Header("Difficulty Panel")]
    public GameObject difficultyPanel;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("WARNING: More than one LevelsController Instance");
            return;
        }

        Instance = this;

        LoadData();
    }

    private void Start()
    {
        
    }

    public void LeftButtonMap()
    {

        if (mapsIndex == 0)
        {
            mapsIndex = maps.Length - 1;
        }
        else
        {
            mapsIndex--;
        }

        //SetUpMapPanel   
        SetUpMap();
    }

    public void RightButtonMap()
    {

        if (mapsIndex == maps.Length - 1)
        {
            mapsIndex = 0;
        }
        else
        {
            mapsIndex++;
        }

        //SetUpMapPanel
        SetUpMap();
    }

    public void SetUpMap()
    {
        mapText.text = maps[mapsIndex].mapName;
        mapsImage.sprite = maps[mapsIndex].mapSprite;
        mapAnim.SetTrigger(changeTrigger);
    }

    //Esta funcion ira dentro del script de cada boton de nivel
    public void ActiveFade()
    {
        BGAudio.Instance.DestroyThisObject();
        fadeEvent.Play(fadeSource);
        fadeController.ActiveBlackHoleFade();
    } 
    
    public void SaveData()
    {
        PlayerPrefs.SetInt("mapsIndex", mapsIndex);
    }

    public void LoadData()
    {
        mapsIndex = PlayerPrefs.GetInt("mapsIndex", 0);
    }

    public void CancelDifficultyButton()
    {
        difficultyPanel.SetActive(false);
    }
}
