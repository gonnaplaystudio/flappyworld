using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutRebotable : MonoBehaviour
{
    [Header("Rebote SetUp")]
    public ParticleSystem jumpParticle;
    public AudioSource jumpAudioSource;

    [Header("Player SetUp")]
    public float reboteForce;
    public Collider thisCollider;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Input.GetMouseButtonDown(0))
        {
            return;
        }

        if(collision.gameObject.CompareTag("Player"))
            PlayerControllerTut.Instance.Rebote(reboteForce);
    }

    public void Jump()
    {
        jumpParticle.Play();
    }
}
