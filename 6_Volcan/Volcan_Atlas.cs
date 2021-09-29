using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

enum VolcanSpriteType
{
    Basic_Square, gem_green, gem_purple, gem_red, Stick, BG, Block, Plat_1, Plat_2 ,rock 
}
public class Volcan_Atlas : MonoBehaviour
{
    [SerializeField]
    private VolcanSpriteType currentType;

    [SerializeField]
    private SpriteAtlas spriteAtlas;

    private SpriteRenderer spriteRend;

    public Gem_TT gem_TT;

    private void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();

        if (gem_TT == null)
        {
            spriteRend.sprite = spriteAtlas.GetSprite(currentType.ToString());
        }
        else
        {
            SetGemSprite(gem_TT.GemSpriteAtlasValue());
        }
    }

    public void SetGemSprite(string _gemValue)
    {
        spriteRend.sprite = spriteAtlas.GetSprite(_gemValue);
    }
}
