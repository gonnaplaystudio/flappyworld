using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public StartGame startGame;

    [Header("Language SetUp")]
    public TutLanguage pressToStartLanguage;
    public TutLanguage tutLanguage;
    public TextMeshProUGUI flappyText;
    public TextMeshProUGUI pressToStartText;

    [Header("Animation SetUp")]
    public Animator thisAnim;
    public string[] animTriggers;
    public Animator bocadilloAnim;
    public string bocadilloTrigger;
    public Animator sticksAnim;
    public GameObject jumpIndicator;

    [Header("Score SetUp")]
    public Image counterImage;
    public Sprite[] levelsType;
    public Animator scoreAnim;
    public string scoreTrigger;
    public GameObject[] arrows;

    private int tutIndex;

    [Header("Audio SetUp")]
    public AudioSource flappySource;
    public AudioEvent flappyTalkEvent;


    private void Awake()
    {
        tutIndex = 0;

        Invoke("ActiveTutorial", 4f);

        SetUpStartText();
    }

    public void NextButton()
    {
        bocadilloAnim.SetTrigger(bocadilloTrigger);
        tutIndex++;

        CheckTutIndex(tutIndex);
        SetFlappyText();
    }

    public void SetUpStartText()
    {
        pressToStartText.text = pressToStartLanguage.GetTutLanguage(0);
    }

    public void CheckTutIndex(int _index)
    {
        switch (_index)
        {
            case 1:
                TalkFlappy();
                break;

            case 2:
                TalkFlappy();
                break;

            case 3:
                //Animacion de salto
                SetAnim(0);
                break;

            case 4:
                TalkFlappy();
                break;

            case 5:
                TalkFlappy();
                break;

            case 6:
                //Se activa la flecha gemas
                SetAnim(1);
                SetUpArrow(0,true);
                TalkFlappy();
                break;

            case 7:
                //Se pone la flecha en el contador
                SetUpArrow(0, false);
                SetUpArrow(1, true);
                TalkFlappy();
                break;

            case 8:
                TalkFlappy();
                ChangeScore(0);
                break;

            case 9:
                TalkFlappy();
                ChangeScore(1);
                break;

            case 10:
                TalkFlappy();
                ChangeScore(2);
                break;

            case 11:
                //Se desactiva la flecha
                SetUpArrow(1, false);
                ChangeScore(0);
                TalkFlappy();
                break;

            case 12:
                TalkFlappy();
                //Se activan los sticks
                sticksAnim.enabled = true;
                break;

            case 13:
                //Se desactiva a Flarbossa
                SetAnim(2);
                PlayerControllerTut.Instance.enabled = true;
                break;

            case 14:
                //Se activa la flecha en las vidas
                TalkFlappy();
                SetUpArrow(2, true);
                break;

            case 15:
                //Se desactiva la flecha y a Flarbosa
                SetUpArrow(2, false);
                SetAnim(2);
                startGame.enabled = true;
                break;

            case 16:
                TalkFlappy();
                sticksAnim.SetTrigger("ExitSticks");
                break;

            case 17:
                TalkFlappy();
                break;

            case 18:
                //Se desactiva a Flarbosa
                SetAnim(2);
                tutIndex = 15;
                startGame.enabled = true;
                sticksAnim.enabled = false;
                break;
        }
    }

    public void ChangeScore(int _index)
    {
        counterImage.sprite = levelsType[_index];
        scoreAnim.SetTrigger(scoreTrigger);
    }

    public void SetFlappyText()
    {
        flappyText.text = tutLanguage.GetTutLanguage(tutIndex);        
    }

    public void SetJumpIndicator()
    {
        jumpIndicator.SetActive(true);
    }

    public void SetAnim(int triggerIndex)
    {
        thisAnim.SetTrigger(animTriggers[triggerIndex]);
    }

    public void SetUpArrow(int _index, bool _value)
    {
        arrows[_index].SetActive(_value);
    }

    public void ActiveTutorial()
    {
        startGame.enabled = false;
        thisAnim.enabled = true;
        
        SetFlappyText();
        TalkFlappy();
    }

    public void BocadilloInAnim()
    {
        bocadilloAnim.SetTrigger(bocadilloTrigger);
    }
    public void TalkFlappy()
    {
        flappyTalkEvent.Play(flappySource);
    }
}
