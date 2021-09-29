using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectorController : MonoBehaviour
{
    public int unlockedIndex;//Hasta donde estaran disponibles los Flappys

    [Header("Coming Soon Panel")]
    public GameObject comingSoonObject;
    public Animator comingSoonAnim;
    public string trigger;

    [Header("Selector Panel SetUp")]
    public TextMeshProUGUI categoryText;
    public TextMeshProUGUI[] flappyNames;
    public Image[] flappyImages;
    public Image[] candadoImages;
    public Image[] flappyMaskImages;
    public Color disabledFlappyColor;
    public Color normalColor;
    public Color transparentColor;
    public Color normalSelectedButtonsColor;

    [Header("Animations")]
    public GameObject[] brights;
    public Animator selectPanelAnim;
    public Animator actualFlappyAnim;
    public string selectedTrigger;
    public string left;
    public string right;
    private bool canChange;
    public float waitTimeToChange;

    [Header("Selected Flappy")]
    public Image selectedFlappyImage;
    public AudioSource flappysSource;
    public AudioEvent flappysEvent;

    [Header("Buttons")]
    public TextMeshProUGUI[] buybuttonsText;
    public Button[] lockedButtons;
    public Button[] selectedButtons;
    public Sprite candado;
    public string lockedString;

    [Header("Buy Panel")]
    public GameObject buyPanel;
    public TextMeshProUGUI panelText;
    public Image buyingFlappyImage;
    private int buyFlappyIndex;
    public GemsController gemsController;
    public Language languageArchive;

    [Header("Presentation Panel")]
    public GameObject presentationPanel;
    public PresentationPanel presentationScript;

    [Header("Scriptable Flappy")]
    public int categoryIndex;
    public int categoriesAmount;
    public Flappy[] actualFlappys;
    public Flappy[] explorersFlappys;
    public Flappy[] fantasyFlappys;
    public Flappy[] warriorsFlappys;
    public Flappy[] musiciansFlappys;
    public Flappy[] monstersFlappys;
    public Flappy[] celebritiesFlappys;
    public Flappy[] comicsFlappys;
    public Flappy[] dragonBallFlappys;
    public Flappy[] videogamesFlappys;

    [Header("Indicator")]
    public Image[] indicatorImages;
    public Color normalIndicatorColor;
    public Color actualIndicatorColor;

    [Header("Bocadillo SetUp")]
    public Animator bocadilloAnim;
    public string bocadilloTrigger;
    public TextMeshProUGUI bocadilloText;
    public TutLanguage bocadilloLanguage;
    public AudioEvent flappyTalkEvent;

    private void Start()
    {
        CheckCategory(0);
        SetCanChange();
        SelectedFlappy(PlayerStats.Instance.GetFlappy());
        SetActualIndicator(0);
        SetBocadilloText();
    }

    public void SetBocadilloText()
    {
        bocadilloText.text = bocadilloLanguage.GetTutLanguage(0);
    }

    public void LeftButton()
    {
        if (!canChange)
            return;

        DisableActualIndicator(categoryIndex);

        if (categoryIndex == 0)
        {
            categoryIndex = categoriesAmount;
        }
        else
        {
            categoryIndex--;
        }

        SetActualIndicator(categoryIndex);

        CheckCategory(categoryIndex);
        selectPanelAnim.SetTrigger(left);
        
        WaitTime();
        DesactiveActualBright();

        UIAudio.Instance.SwipeFx();

        ComingSoonAnim();
    }

    public void RightButton()
    {
        if (!canChange)
            return;

        DisableActualIndicator(categoryIndex);

        if (categoryIndex == categoriesAmount)
        {
            categoryIndex = 0;
        }
        else
        {
            categoryIndex++;
        }

        SetActualIndicator(categoryIndex);

        CheckCategory(categoryIndex);
        selectPanelAnim.SetTrigger(right);
        
        WaitTime();
        DesactiveActualBright();

        UIAudio.Instance.SwipeFx();

        ComingSoonAnim();
    }

    public void ComingSoonAnim()
    {
        if (comingSoonObject.activeInHierarchy)
            comingSoonAnim.SetTrigger(trigger);
    }

    //Para una animacion correcta
    public void WaitTime()
    {
        canChange = false;
        Invoke("SetCanChange", waitTimeToChange);
    }

    public void SetCanChange()
    {
        canChange = true;
    }

    public void CheckCategory(int _categoryIndex)
    {
        switch (_categoryIndex)
        {
            case 0:
                SetActualFlappys(explorersFlappys);
                SetUpPanel();
                break;
            case 1:
                SetActualFlappys(fantasyFlappys);
                SetUpPanel();
                break;
            case 2:
                SetActualFlappys(warriorsFlappys);
                SetUpPanel();
                break;
            case 3:
                SetActualFlappys(musiciansFlappys);
                SetUpPanel();
                break;
            case 4:
                SetActualFlappys(monstersFlappys);
                SetUpPanel();
                break;
            case 5:
                SetActualFlappys(celebritiesFlappys);
                SetUpPanel();
                break;
            case 6:
                SetActualFlappys(comicsFlappys);
                SetUpPanel();
                break;
            case 7:
                SetActualFlappys(dragonBallFlappys);
                SetUpPanel();
                break;
            case 8:
                SetActualFlappys(videogamesFlappys);
                SetUpPanel();
                break;
            default:
                Debug.Log("Error");
                break;
        }
    }

    public void DisableActualIndicator(int _index)
    {
        indicatorImages[_index].color = normalIndicatorColor;
    }

    public void SetActualIndicator(int _index)
    {
        indicatorImages[_index].color = actualIndicatorColor;
    }

    public void SetActualFlappys(Flappy[] _newFlappys)
    {
        actualFlappys = _newFlappys;
    }

    public void SetUpPanel()
    {
        //Hace la animacion de cambio de categoria

        //Muestra la categoria
        categoryText.text = actualFlappys[0].pack;

        //Para bloqueo o desbloquedo
        for (int i = 0; i < actualFlappys.Length; i++)
        {

            //Si es un Flappy por trofeo
            if (actualFlappys[i].id > unlockedIndex)
            {
                LockedFlappy(actualFlappys[i], i);
                comingSoonObject.SetActive(true);
            }
            else
            {
                //Se muestra desbloqueado
                UnlockedFlappy(actualFlappys[i], i);
                comingSoonObject.SetActive(false);
            }
        }
    }

    public void LockedFlappy(Flappy _newFlappy, int _index)
    {
        candadoImages[_index].color = normalColor;
        flappyImages[_index].color = transparentColor;        
        lockedButtons[_index].gameObject.SetActive(true);
        lockedButtons[_index].interactable = false;
    }

    public void UnlockedFlappy(Flappy _newFlappy, int _index)
    {
        //Comrpueba si esta comprado
        if (PlayerStats.Instance.GetFlappyShop(_newFlappy.id))
        {
            SetBoughtFlappy(_newFlappy, _index);
            //Desactiva el boton de compra
            lockedButtons[_index].gameObject.SetActive(false);
            selectedButtons[_index].gameObject.SetActive(true);
            flappyNames[_index].text = _newFlappy.flappyName;

            //COMPRUEBA SI ES EL FLAPPY SELECCIONADO
            if(PlayerStats.Instance.GetFlappy().id == _newFlappy.id)
            {
                selectedButtons[_index].image.color = transparentColor;
            }
            else
            {
                selectedButtons[_index].image.color = normalSelectedButtonsColor;
            }
        }
        else
        {
            SetToBuyFlappy(_newFlappy, _index);
            //Activa el boton de compra
            lockedButtons[_index].gameObject.SetActive(true);
            selectedButtons[_index].gameObject.SetActive(false);
            selectedButtons[_index].image.color = normalSelectedButtonsColor;
            buybuttonsText[_index].text = "???";
            //lockedButtons[_index].interactable = true;
        }
    }

    public void SetBoughtFlappy(Flappy _newFlappy, int _index)
    {
        candadoImages[_index].color = transparentColor;
        flappyImages[_index].sprite = _newFlappy.flappySprite;
        flappyMaskImages[_index].color = normalColor;
    }

    public void SetToBuyFlappy(Flappy _newFlappy, int _index)
    {
        candadoImages[_index].color = transparentColor;
        flappyImages[_index].sprite = _newFlappy.flappySprite;
        flappyMaskImages[_index].color = disabledFlappyColor;
    }

    public void NotEnoughtGems()
    {
        bocadilloAnim.SetTrigger(bocadilloTrigger);
        flappyTalkEvent.Play(flappysSource, PlayerStats.Instance.GetFlappy().voicePitch);
    }

    //Para los botones de compra
    public void BuyFlappyButtons(int _buyFlappyIndex)
    {
        UIAudio.Instance.ButtonFx();

        NotEnoughtGems();

        /*
        //Comprobar que hay suficientes gemas
        if (actualFlappys[_buyFlappyIndex].price > PlayerStats.Instance.GetGems())
        {
            //No hay suficientes
            //Activar el texto de gemas insuficientes
            //Activar el sonido de cancel 
        }
        else
        {
            //Se activa el panel de compra
            BuyPanel(_buyFlappyIndex);
            //Para saber cual flappy es el que compra 
            buyFlappyIndex = _buyFlappyIndex;            
        }
        */
    }

    #region BuyPanel
    public void BuyPanel(int _FlappyIndex)
    {
        SetBuyPanel(true);
        buyingFlappyImage.sprite = actualFlappys[_FlappyIndex].flappySprite;
        panelText.text = languageArchive.BuyFlappyText(actualFlappys[_FlappyIndex].flappyName,
            actualFlappys[_FlappyIndex].price);
    }

    //El boton de comprar en el panel de comprar
    public void BuyButton()
    {
        //Se resta el dinero en el UI de gemas
        gemsController.SetGems(actualFlappys[buyFlappyIndex].price);
        //Se restan las gemas
        PlayerStats.Instance.SetGems(-actualFlappys[buyFlappyIndex].price);
        //En el caso de que haya guardar el dato de compra
        PlayerStats.Instance.SetFlappyShop(actualFlappys[buyFlappyIndex].id, true);
        //Desactivar el boton de compra
        lockedButtons[buyFlappyIndex].gameObject.SetActive(false);
        //Se desactiva el panel de compra
        SetBuyPanel(false);

        //Presentacion Flappy
        presentationPanel.SetActive(true);
        presentationScript.SetFlappy(actualFlappys[buyFlappyIndex]);

        SetUpPanel();

        PlayerStats.Instance.AddFlappysAmount();

        UIAudio.Instance.ButtonFx();

        Data.Instance.SaveData();
    }

    public void PlayBrightEffect()
    {
        //Salen particulas de compra
        brights[buyFlappyIndex].SetActive(true);
        WaitTime();
    }

    //Boton de cancelar la compra
    public void CancelBuyButton()
    {
        //Se desactiva el panel de compra
        SetBuyPanel(false);
        UIAudio.Instance.ButtonFx();
    }

    //Para activar o desactivar el panel de compra
    public void SetBuyPanel(bool _value)
    {
        buyPanel.SetActive(_value);
    }

    #endregion

    public void DesactiveActualBright()
    {
        if (brights[buyFlappyIndex].activeInHierarchy)
            brights[buyFlappyIndex].SetActive(false);
    }

    public void SelectedFlappy(Flappy _actualFlappy)
    {
        //Se muestra el flappy seleccionado
        selectedFlappyImage.sprite = _actualFlappy.flappySprite;
    }

    //Para los botones de seleccion(los que ya estan comprados)
    public void SelectFlappy(int _actualFlappyIndex)
    {
        PlayerStats.Instance.SetFlappy(actualFlappys[_actualFlappyIndex]);
        SelectedFlappy(PlayerStats.Instance.GetFlappy());
        actualFlappyAnim.SetTrigger(selectedTrigger);

        flappysEvent.Play(flappysSource, actualFlappys[_actualFlappyIndex].voicePitch);

        SetUpPanel();

        UIAudio.Instance.ButtonFx();

        Data.Instance.SaveData();
    }
}
