using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

enum TutorialSpriteType
{
    Basic_Square,Delimitadores, Fondo, Stick
}
public class TutorialAtlas : MonoBehaviour
{
    [SerializeField]
    private TutorialSpriteType currentType;

    [SerializeField]
    private SpriteAtlas spriteAtlas;

    private SpriteRenderer spriteRend;

    private void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();

        spriteRend.sprite = spriteAtlas.GetSprite(currentType.ToString());
    }
}
