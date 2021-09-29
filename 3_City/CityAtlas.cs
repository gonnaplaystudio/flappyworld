using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

enum CitySpriteType
{
    Arrow, BasicSquare,BackGround, Delimitadores, Edificios, Semaforo, Sierra, City_BG, gem_green, gem_purple, gem_red, Z_Block
}
public class CityAtlas : MonoBehaviour
{
    [SerializeField]
    private CitySpriteType currentType;

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
