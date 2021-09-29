using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTut : MonoBehaviour
{
    public static PlayerControllerTut Instance;

    [Header("Jump Attributes")]
    public Rigidbody2D rig;
    public float startMoveForce;
    public float actualMoveForce;
    public float fallSpeed;
    public float fowardForce;
    public float upForce;
    public Animator spriteAnim;
    public ParticleSystem jumpParticles;
    public ParticleSystem.MainModule jumpMainParticles;

    [Header("Die Attributes")]
    public SpriteRenderer characterSprite;
    public Color dieColor;
    public Collider2D thisCollider;
    public Transform particlesTransform;
    public ParticleSystem dieParticles;
    public ParticleSystem.MainModule dieParticlesMinMax;
    public TutController tutController;
    public Transform lostGemTranform;
    public ParticleSystem lostGemParticles;

    [Header("Triggers")]
    public string left;
    public string right;
    public string stick;
    public string jump;
    public string staticSprite;

    [Header("Audio SetUp")]
    public AudioSource thisAudio;
    public AudioEvent jumpEvent;
    public AudioEvent dieEvent;
    public AudioEvent wallsEvent;

    public bool dead;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("WARNING:More than one PlayerControllerTut Instance");
            return;
        }

        Instance = this;

        SetUpPlayer(PlayerStats.Instance.GetFlappy());
    }

    void Start()
    {
        SetStartVelocity();
    }

    private void OnEnable()
    {
        rig.simulated = true;
        this.thisCollider.enabled = true;
        Jump();
    }

    void Update()
    {
        if (dead != true)
        {
            if (Input.GetMouseButtonDown(0) && Time.timeScale == 1)
            {
                Jump();
            }

            rig.velocity = new Vector2(actualMoveForce, rig.velocity.y - Time.deltaTime * fallSpeed);
        }
    }

    public void Jump()
    {
        spriteAnim.SetTrigger(jump);

        jumpParticles.Play();

        rig.velocity = Vector3.zero;
        rig.AddForce(new Vector2(fowardForce, upForce));

        jumpEvent.Play(thisAudio);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (actualMoveForce <= 0)
            {
                fowardForce = -fowardForce;
                actualMoveForce = -actualMoveForce;
                transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            }
            else if(actualMoveForce >= 0)
            {
                fowardForce = -fowardForce;
                actualMoveForce = -actualMoveForce;
                transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
            }

            wallsEvent.Play(thisAudio);
        }

        if (collision.gameObject.CompareTag("Stick"))
        {
            Die();
            tutController.PlayerDead();
        }            
    }

    public void Rebote(float auxForce)
    {
        rig.velocity = Vector3.zero;
        rig.AddForce(new Vector2(fowardForce, auxForce));
    }

    public void Die()
    {
        //ActiveDieSound
        dieEvent.Play(thisAudio);

        characterSprite.color = dieColor;
        thisCollider.enabled = false;
        rig.simulated = false;

        //Active dieParticles
        SetDieParticles();

        lostGemTranform.position = this.transform.position;
        lostGemParticles.emission.SetBurst(0, new ParticleSystem.Burst(0, tutController.GetGemsAmount()/2));
        lostGemParticles.Play();

        SetStartVelocity();

        //Desactivar este script
        this.enabled = false;
    }

    public void SetDieParticles()
    {
        particlesTransform.position = this.transform.position;
        dieParticles.Play();
    }

    public void SetVelocity(float _newVelocity)
    {
        if (actualMoveForce < 0)
        {
            actualMoveForce -= _newVelocity;
        }
        else
        {
            actualMoveForce += _newVelocity;
        }
    }

    public void SetStartVelocity()
    {
        if (actualMoveForce < 0)
        {
            actualMoveForce = -startMoveForce;
        }
        else
        {
            actualMoveForce = startMoveForce;
        }
    }

    public void SetUpPlayer(Flappy _playerStatsFlappy)
    {
        jumpMainParticles = jumpParticles.main;
        dieParticlesMinMax = dieParticles.main;

        characterSprite.sprite = _playerStatsFlappy.flappySprite;
        jumpMainParticles.startColor = _playerStatsFlappy.jumpColor;

        dieParticlesMinMax.startColor = new ParticleSystem.MinMaxGradient(_playerStatsFlappy.dieColor1,
            _playerStatsFlappy.dieColor2);
    }

    public void StopPlayer()
    {
        thisCollider.enabled = false;
        rig.simulated = false;

        spriteAnim.SetTrigger(staticSprite);

        this.enabled = false;
    }

    public void EnterTheHole()
    {
        thisCollider.enabled = false;
        rig.simulated = false;
        spriteAnim.SetTrigger("EnterHole");
        this.enabled = false;
    }
}
