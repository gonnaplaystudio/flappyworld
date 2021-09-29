using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//Auxiliar

public class FadeController : MonoBehaviour
{
    public GameObject blackHoldeFade;
    public GameObject enterSceneFade;

    private void Awake()
    {
        CheckEnterScene();
    }

    public void CheckEnterScene()
    {
        if (PlayerStats.Instance.GetFadeFromLevels())
        {
            BGAudio.Instance.PlayMainSountrack();
        }

        enterSceneFade.SetActive(PlayerStats.Instance.GetFadeFromLevels());
        PlayerStats.Instance.SetFadeFromLevels(false);
    }

    public void ActiveBlackHoleFade()
    {
        blackHoldeFade.SetActive(true);
    }
}
