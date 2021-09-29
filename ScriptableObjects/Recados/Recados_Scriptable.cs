using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recado", menuName = "Recado")]
public class Recados_Scriptable : ScriptableObject
{
    public int index;
    public TutLanguage recadosText;
    public Sprite flappy;
    public int price;
    public int reward;
}
