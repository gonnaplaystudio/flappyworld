using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

enum SpriteType_Clouds
{
    Basic_Square, Big_Cloud_1, Big_Cloud_2, Big_Cloud_3, Cloud_Rebotable, gem_green, gem_purple, gem_red, 
    Moon, Rocks, Stick, Wall
}
public class CloudsAtlas_Big : MonoBehaviour
{
    [SerializeField]
    private SpriteType_Clouds currentType;

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
