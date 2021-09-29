using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

enum FlaptureSpriteType
{
    Basic_Square, gem_green, gem_purple, gem_red, Laser, Laser_Haz, BG_Water, Delimitador_S, Delimitador_L, Block_1 ,Block_2,
        Block_Large, Laser_2, Z_Delimitador
}
public class FlaptureAtlas : MonoBehaviour
{
    [SerializeField]
    private FlaptureSpriteType currentType;

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
