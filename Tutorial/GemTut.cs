using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemTut : MonoBehaviour
{
    [Header("Gem SetUp")]
    public TutController tutController;
    public Animator gemValueAnim;
    public Transform gemValueTranform;
    public GameObject parentObj;

    [Header("Audio SetUp")]
    public AudioSource thisAudio;
    public AudioEvent pickCointEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tutController.AddGem();
            SetValue();
            parentObj.SetActive(false);

            pickCointEvent.Play(thisAudio);
        }
    }

    public void SetValue()
    {
        gemValueAnim.SetTrigger("Active");
        gemValueTranform.position = transform.position;
    }
}
