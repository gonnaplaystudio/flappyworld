using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualGem : MonoBehaviour
{
    [Header("Gem SetUp")]
    public Gem gemController;
    public int gemValue;
    public AudioInLevels audioLevels;

    [Header("Gems Particles SetUp")]
    public Transform actualValueTransform;
    public Animator actualValueAnim;
    public Transform[] valueTranform;
    public Animator[] valueAnim;
    public string activeTrigger;

    [Header("Gems Sprite SetUp")]
    public SpriteRenderer thisRend;

    private void Awake()
    {
        thisRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioLevels.GemAudio();
            //Se añadira la puntuacion
            gemController.AddGem(gemValue);
            //Se instanciaran las particulas de value
            ShowValue();
            //Se desactivara la actual gema
            Desactive();
        }
    }

    public void ShowValue()
    {
        actualValueTransform.position = transform.position;
        actualValueAnim.SetTrigger(activeTrigger);
    }

    public void SetGemValue(int _newValue)
    {
        gemValue = _newValue;
    }

    public void SetGemSprites(int _index, Sprite _newSprite)
    {
        thisRend.sprite = _newSprite;
        actualValueAnim = valueAnim[_index];
        actualValueTransform = valueTranform[_index];
    }

    public void Desactive()
    {
        gemController.CheckDesactive();
        this.transform.parent.gameObject.SetActive(false);
    }
}
