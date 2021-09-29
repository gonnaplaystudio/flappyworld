using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Difficulty : MonoBehaviour
{
    [Header("Score Attributes")]
    public TextMeshProUGUI scoreText;
    private int counter;

    [Header("Player Attributes")]
    public string difficultyMode;
    public DifficultyScriptable difficultyScriptable;
    public int[] counterVelocity;//Aumento por cada rebote
    private int counterVelocityIndex;//Index para el array de velocidad
    public float increaseVelocity;
    public Player_Controller playerController;

    [Header("Black Hole Attributes")]
    public Animator blackHoleAnim;
    public string endHoleTrigger;
    public string desactiveHole;
    private bool activeHole;

    [Header("Gems SetUp")]
    public Gem gemControllerScript;
    public GameObject gemController;
    public Transform gemTransform;
    public Transform leftParent;
    public Transform rightParent;
    private Transform[] leftSpawners;
    private Transform[] rightSpawners;
    private int gemsIndex;
    public int[] addGemValueIndex;

    [Header("Audio")]
    public AudioInLevels audioLevels;

    private void Start()
    {
        SetStartCounter();
        GetSpawners();
        SetPlayerStartMoveForce();
    }

    //Sirve tanto para el inicio como para Reset
    public void SetStartCounter()
    {
        //Coge el difficultyMode de playerStats
        difficultyMode = PlayerStats.Instance.GetDifficulty();

        counter = difficultyScriptable.GetStartGoal(difficultyMode);
        counterVelocity = difficultyScriptable.GetIndexDeAumento(difficultyMode);
        counterVelocityIndex = 0;
        gemsIndex = 0;
        SetScore();
    }

    //Para cuando el jugador choque contra una pared y le pase un int booleano para saber que lado choca
    public void RestCounter(int _side)
    {
        if (counter == 0)
            return;

        counter--;
        gemsIndex++;

        audioLevels.CounterAudio();

        SetScore();
        CheckVelocity();

        if(counter > 0)
            SetGem(_side);

        //Colocara la gema en el lado correspondiente

        if(counter == 0)
        {
            //Se activara la recompensa
            SetActiveHole(true);
            SetUpEndHole();
        }
    }

    public void SetScore()
    {
        scoreText.text = counter.ToString();
    }

    //Aumenta la velocidad del player
    public void CheckVelocity()
    {
        if (counterVelocityIndex == counterVelocity.Length)
            return;

        if(counter == counterVelocity[counterVelocityIndex])
        {
            playerController.SetVelocity(increaseVelocity);
            counterVelocityIndex++;
        }
    }

    public void SetUpEndHole()
    {
        blackHoleAnim.SetTrigger(endHoleTrigger);
    }

    public void DesactiveEndHole()
    {
        blackHoleAnim.SetTrigger(desactiveHole);
    }

    public void SetActiveHole(bool _value)
    {
        activeHole = _value;
    }

    public bool GetActive()
    {
        return activeHole;
    }

    public void SetGem(int _side)
    {
        if (!gemController.activeInHierarchy)
        {
            gemController.SetActive(true);
            gemControllerScript.ActiveGems();

            switch (_side)
            {
                case 0://Si el lado es izquierdo donde choca el player
                    gemTransform.position = rightSpawners[GetRandomNumber()].position;
                    gemTransform.rotation = new Quaternion(0, 0, 0, 0);
                    break;
                case 1://Si el lado es derecho donde choca el player
                    gemTransform.position = leftSpawners[GetRandomNumber()].position;
                    gemTransform.rotation = new Quaternion(0, 180, 0, 0);
                    break;
                default:
                    return;
            }

            CheckGemValue();
        }
    }

    public void CheckGemValue()
    {
        if(gemsIndex > addGemValueIndex[0])
        {
            gemControllerScript.SetGemValue(2);
        }

        if(gemsIndex > addGemValueIndex[1])
        {
            gemControllerScript.SetGemValue(3);
        }
    }

    public void GetSpawners()
    {
        leftSpawners = new Transform[leftParent.childCount];
        rightSpawners = new Transform[rightParent.childCount];

        for (int i = 0; i < leftSpawners.Length; i++)
        {
            leftSpawners[i] = leftParent.GetChild(i);
        }

        for (int i = 0; i < rightSpawners.Length; i++)
        {
            rightSpawners[i] = rightParent.GetChild(i);
        }
    }

    public int GetRandomNumber()
    {
        int randomNumber = (int)Random.Range(0f, rightSpawners.Length);

        return randomNumber;
    }

    public void SetPlayerStartMoveForce()
    {
        playerController.SetStartMoveForce(difficultyScriptable.GetStartVelocity(difficultyMode));
    }
}
