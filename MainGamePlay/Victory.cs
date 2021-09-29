using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Victory : MonoBehaviour
{
    public static Victory Instance;
     
    [Header("Victory Canvas")]
    public Animator victoryCanvasAnim;
    public string exitTrigger;
    public int nextLevelIndex;
    public int thisSceneIndex;
    public Button[] victoryButtons;
    public GameObject addButton;
    private Button add_Button;
    private int indexButtons;

    [Header("Timer for Gems")]
    public TextMeshProUGUI totalGemsText;
    public TextMeshProUGUI levelGemsText;
    private bool doneGemsTransiction;

    [Header("Gems SetUp")]
    public Gem gemScript;
    public Gems_TT_Controller gem_TT_Controller;
    public int actualGemsAmount;
    private int gemsToPlayer;
    public int levelGemsAmount;
    public int doubleGemValue;
    private int resultGems;
    private float coroutineGemsTime;

    [Header("Levels Type")]
    public bool vsLevel;
    public int VS_gems;
    public bool bossLevel;
    public Boss_Clouds bossScript;
    public string levelType;
    public int levelIndex;

    [Header("Language SetUp")]
    public TextMeshProUGUI moreGemsText;
    public TextMeshProUGUI victoryText;
    public TutLanguage victoryLanguage;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Warning: More than one Victory Instance");
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        add_Button = addButton.GetComponent<Button>();
        SetVictoryText();
        SetGems();
        ShowGemsAmount();
        CheckPause();

        NotificationIndex();

        Analiticas.Instance.LevelCompleteAnalitycs(PlayerStats.Instance.GetDifficulty(), levelGemsAmount);
    }

    public void SetVictoryText()
    {
        moreGemsText.text = victoryLanguage.GetTutLanguage(0);
        victoryText.text = victoryLanguage.GetTutLanguage(1);
    }

    public void SetGemsAnimationInMap()
    {
        PlayerPrefs.SetString("GemsAnim", "true");
        PlayerPrefs.SetInt("GemsAmount", gemsToPlayer);

        //NivelCompletado(Cambio de nivel)
        if (!vsLevel)
        {
            PlayerPrefs.SetString("LevelCompleteType", levelType);
            PlayerPrefs.SetInt("LevelCompleteIndex", levelIndex);
        }
        else
        {
            PlayerPrefs.SetString("LevelCompleteType", "VS");
        }        
    }

    public void CheckPause()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        PauseControll.Instance.enabled = false;
    }

    public void SetUpSlider()
    {
       
    }

    #region GemsAnimation
    public IEnumerator GemsAnim()
    {
        PlayerStats.Instance.SetGems(gemsToPlayer);
        PlayerStats.Instance.SetTotalGems(gemsToPlayer);

        CheckGemsAmount(gemsToPlayer);

        SetUpButtons(false);

        while (levelGemsAmount > 0)
        {
            actualGemsAmount += 1;
            levelGemsAmount -= 1;

            ShowGemsAmount();
            yield return new WaitForSeconds(coroutineGemsTime);
        }

        //Desactivar el canvas
        Invoke("DesactiveVictoryCanvas", 1f);

        //Se guardan los datos
        Data.Instance.SaveData();

        SetGemsAnimationInMap();

        StopCoroutine(GemsAnim());
    }

    public void SetCoroutineGemsTime(float _time)
    {
        coroutineGemsTime = _time;
    }

    public void CheckGemsAmount(int _gemsAmount)
    {
        if(_gemsAmount >= 50)
        {
            SetCoroutineGemsTime(.007f);
        }
        else
        {
            SetCoroutineGemsTime(.01f);
        }
    }

    public IEnumerator DoubleGemsAnim()
    {
        //addButton.SetActive(false);

        SetAddButton(false);

        doneGemsTransiction = true;
        SetUpButtons(false);

        doubleGemValue = levelGemsAmount * 2;
        gemsToPlayer = doubleGemValue;

        //Mostrar el simbolo X2

        while (levelGemsAmount < doubleGemValue)
        {
            levelGemsAmount += 1;

            ShowGemsAmount();
            yield return new WaitForSeconds(.01f);
        }

        SetUpButtons(true);
        StopCoroutine(DoubleGemsAnim());
    }

    public void SetAddButton(bool _value)
    {
        add_Button.interactable =  _value;
    }

    public void ShowGemsAmount()
    {
        totalGemsText.text = actualGemsAmount.ToString();
        levelGemsText.text = levelGemsAmount.ToString();
    }

    public void SetGems()
    {
        if (gemScript != null)
        {
            levelGemsAmount = gemScript.GetGemsAmount();
        }
        else if(gem_TT_Controller != null)
        {            
            levelGemsAmount = gem_TT_Controller.GetGemsAmount();
        }

        CheckVS();

        CheckBoss();
        
        actualGemsAmount = PlayerStats.Instance.GetGems();
        gemsToPlayer = levelGemsAmount;
    }

    public void CheckVS()
    {
        if (vsLevel)
        {
            levelGemsAmount = VS_gems;
        }
    }

    public void CheckBoss()
    {
        if (bossLevel)
        {
            levelGemsAmount = bossScript.GetGemsAmount();
        }
    }

    public void ExitScene()
    {
        Invoke("DesactiveVictoryCanvas", 2f);
    }

    #endregion

    #region Buttons Set Up

    #region Buttons
    public void ThisLevelButton()
    {
        indexButtons = 0;
        StartCoroutine(GemsAnim());
    }

    public void NextLevelButton()
    {
        indexButtons = 1;
        StartCoroutine(GemsAnim());
    }

    public void MenuButton()
    {
        SetAddButton(false);
        indexButtons = 2;
        StartCoroutine(GemsAnim());
    }

    public void AddButton_2()
    {
        AnuncioController.Instance.RewardVideo("gems");
    }

    public void AddButton()
    {
        if (doneGemsTransiction)
            return;


        StartCoroutine(DoubleGemsAnim());

        //AnuncioController.Instance.GamePlayVideo();

        Analiticas.Instance.GemsAdd();
    }

    public void AddButtonInGeneric()//QUITAR
    {
        if (doneGemsTransiction)
            return;

        //Aqui se instanciara el anuncio desde otro script
        StartCoroutine(DoubleGemsAnim());

        Analiticas.Instance.GemsAdd();
    }
    #endregion

    public void CheckSceneToLoad()
    {
        switch (indexButtons)
        {
            case 0:
                LoadActualLevel();
                break;

            case 1:
                LoadNextLevel();
                break;

            case 2:
                LoadMainMenu();
                break;
        }
    }

    public void LoadActualLevel()
    {
        SceneManager.LoadScene(thisSceneIndex);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelIndex);
    }

    public void LoadMainMenu()
    {
        PlayerStats.Instance.SetFadeFromLevels(true);
        SceneManager.LoadScene(3);
    }

    public void DesactiveVictoryCanvas()
    {
        victoryCanvasAnim.SetTrigger(exitTrigger);
        Invoke("CheckSceneToLoad", 3f);
    }

    public void SetUpButtons(bool _value)
    {
        victoryButtons[1].interactable = _value; 
    }
    
    #endregion

    public void NotificationIndex()
    {
        int aux = PlayerPrefs.GetInt("NotificationIndex", 0);

        aux++;

        PlayerPrefs.SetInt("NotificationIndex", aux);
    }
}
