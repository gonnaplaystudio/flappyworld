using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [Header("Difficulty Panel")]
    public GameObject difficultyPanel;
    public int thisLevelIndex;

    [Header("Button SetUp")]
    public MainSettings mainSettings;
    public ButtonsContainer buttonsContainer;


    public void Pressed()
    {
        //Aqui se tiene que controlar si le quedan vidas al jugador
        //Si no le quedan vidas se abre el panel de ver un anuncio
        if(PlayerStats.Instance.GetLifes() > 0)
        {
            //Le envia la scene index a los botones de dificultad
            difficultyPanel.SetActive(true);
            buttonsContainer.SetSceneIndex(thisLevelIndex);            
        }
        else
        {
            //Abre el panel de vidas
            mainSettings.LifePanelButton();
        }

        UIAudio.Instance.ButtonFx();
    }
}
