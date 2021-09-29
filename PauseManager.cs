using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public Animator fadeAnim;
    public string fadeTrigger;
    public GameObject pauseCanvas;
    public PauseControll pauseManager;
    public PlayerControllerTut playerController;
    public int levelsScene;

    public void ResumeButton()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
    }

    public void ExitButton()
    {
        Time.timeScale = 1;
        fadeAnim.SetTrigger(fadeTrigger);

        pauseCanvas.SetActive(false);

        pauseManager.enabled = false;
        playerController.StopPlayer();

        PlayerStats.Instance.SetFadeFromLevels(true);

        Invoke("ChangeScene", 2f);

        PlayerPrefs.SetString("FirstGame", "false");
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(levelsScene);
    }
}
