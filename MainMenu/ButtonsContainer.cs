using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsContainer : MonoBehaviour
{
    //Le dice a los botones de dificultad que nivel cargar

    public DifficultyButton[] difficultyButtons;

    public void SetSceneIndex(int _sceneIndex)
    {
        for (int i = 0; i < difficultyButtons.Length; i++)
        {
            difficultyButtons[i].SetSceneIndex(_sceneIndex);
        }
    }
}
