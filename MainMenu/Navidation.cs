using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navidation : MonoBehaviour
{
    public GameObject navigationFade;
    private Scene thisScene;

    public Animator fadeAnim;
    public string fadeTrigger;
    public float timeToWait;

    public int levelSceneIndex;
    public int selectorSceneIndex;
    public int flappygram;

    private bool interactable;

    private void Awake()
    {
        interactable = true;
    }

    private void Start()
    {
        thisScene = SceneManager.GetActiveScene();

        if (thisScene.buildIndex == levelSceneIndex)
        {
            if (PlayerStats.Instance.GetNavigationFade())
                navigationFade.SetActive(true);
        }
    }

    public void PressButton(string _levelToLoad)
    {
        if (!interactable)
            return;

        UIAudio.Instance.ButtonFx();

        //Activa el fade
        CheckFadeInLevels();
        fadeAnim.SetTrigger(fadeTrigger);

        //Pone interactable en false para que una vez presionado el boton no haga nada mas
        interactable = false;

        switch (_levelToLoad)
        {
            case "Levels":
                Invoke("LoadLevelScene", timeToWait);
                PlayerStats.Instance.SetNavigationFade(true);
                break;

            case "Selector":
                Invoke("LoadSelectorScene", timeToWait);
                break;

            case "Flappygram":
                Invoke("LoadFlappygramScene", timeToWait);
                break;

            default:
                Debug.Log("Error in navigation Index");
                break;
        }
    }

    public void CheckFadeInLevels()
    {
        if (!navigationFade.activeInHierarchy)
        {
            navigationFade.SetActive(true);
        }
    }

    public void LoadLevelScene()
    {
        SceneManager.LoadScene(levelSceneIndex);
    }

    public void LoadSelectorScene()
    {
        SceneManager.LoadScene(selectorSceneIndex);
    }

    public void LoadFlappygramScene()
    {
        SceneManager.LoadScene(flappygram);
    }
}
