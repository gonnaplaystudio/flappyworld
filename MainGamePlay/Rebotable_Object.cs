using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebotable_Object : MonoBehaviour
{
    [Header("Rebote SetUp")]
    public ParticleSystem jumpParticle;

    [Header("Player SetUp")]
    public float reboteForce;
    public Collider thisCollider;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioEvent reboteEvent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Input.GetMouseButtonDown(0))
        {
            return;
        }

        AudioRebote();

        Player_Controller.Instance.Rebote(reboteForce);
        Player_Controller.Instance.SetOnRebotable(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Player_Controller.Instance.SetOnRebotable(false);
    }

    public void Jump()
    {
        jumpParticle.Play();
    }

    public void AudioRebote()
    {
        reboteEvent.Play(audioSource);
    }
}
