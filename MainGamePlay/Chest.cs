using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class Chest : MonoBehaviour
{
    [Header("Save Data")]
    public int saveDataindex;

    [Header("Analytics")]
    public string levelName;

    [Header("Chest Animation")]
    public Animator thisAnim;
    public string chestTrigger;
    public Sprite gemsSprite;
    public Sprite starSprite;
    public Renderer rewardRend;

    public Player_Controller playerController;

    [Header("Victory Panel")]
    public Animator fadeInLevelsAnim;
    public string exitFadeTrigger;
    public GameObject victoryCanvas;
    public float waitTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //QUITAR
            Debug.Log("Stop");  

            playerController.EnterHole();
            Victory();
            CheckDifficulty(PlayerStats.Instance.GetDifficulty());

            Analytics.CustomEvent(levelName, new Dictionary<string, object>
            {
                {"Flappy",PlayerStats.Instance.GetFlappy().flappyName },
                {"gems",PlayerStats.Instance.GetGems() },
                {"lifes",PlayerStats.Instance.GetLifes() }                
            });
        }
    }

    public void Victory()
    {
        Invoke("ActiveVictoryPanel", waitTime);
    }

    public void ActiveVictoryPanel()
    {
        fadeInLevelsAnim.SetTrigger(exitFadeTrigger);
        Invoke("ActiveVictoryCanvas", 4f);
    }

    public void ActiveVictoryCanvas()
    {
        victoryCanvas.SetActive(true);
    }

    public void CheckDifficulty(string _difficulty)
    {
        switch (_difficulty)
        {
            case "Easy":
                PlayerStats.Instance.SetEasyLevel(saveDataindex, true);
                break;

            case "Normal":
                PlayerStats.Instance.SetNormalLevel(saveDataindex, true);
                break;

            case "Hard":
                PlayerStats.Instance.SetHardLevel(saveDataindex, true);
                break;
        }
    }
}
