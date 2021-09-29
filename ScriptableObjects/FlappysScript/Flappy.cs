using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Flappy", menuName = "Flappy")]
public class Flappy : ScriptableObject
{
    [Header("UI Attributtes")]
    public int id;
    public string pack;
    public string flappyName;
    public Color displayPanelColor;
    public Sprite flappySprite;
    public int price;
    public float voicePitch;
    //Esto sirve para los logros
    public bool achievement;
    public int achievementIndex;

    [Header("Game Play Attributes")]
    public Color jumpColor;
    public Color dieColor1;
    public Color dieColor2;
    public int percentageAttack;
    public float hitRatio;
}
