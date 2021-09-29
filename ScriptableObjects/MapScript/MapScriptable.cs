using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Map", menuName = "Map")]
public class MapScriptable : ScriptableObject
{
    public Sprite mapSprite;
    public string mapName;
}
