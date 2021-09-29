using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

enum SpriteType {BackGround ,Basic_Square, Cloud_BG_1, Cloud_BG_2, Cloud_BG_3, Cloud_Rebotable, Delimitadores,
     Moon, Stick }
public class CloudsAtlas : MonoBehaviour
{
    [SerializeField]
    private SpriteType currentType;

    [SerializeField]
    private SpriteAtlas spriteAtlas;

    private SpriteRenderer spriteRend;

    private void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();

        spriteRend.sprite = spriteAtlas.GetSprite(currentType.ToString());
    }
}
