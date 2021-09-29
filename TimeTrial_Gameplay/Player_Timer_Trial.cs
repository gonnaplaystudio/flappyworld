using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Timer_Trial : MonoBehaviour
{
    public static Player_Timer_Trial Instance;

    [Header("Audio")]
    public AudioInLevels audioLevels;

    [Header("Jump Attributes")]
    public Rigidbody2D rig;
    public float startMoveForce;
    public float actualMoveForce;
    public float fallSpeed;
    public float fowardForce;
    public float upForce;
    public Animator spriteAnim;
    public Difficulty difficulty;
    public ParticleSystem jumpParticles;
    public ParticleSystem.MainModule jumpMainParticles;
    private bool onRebotableObject;

    [Header("Die Attributes")]
    public SpriteRenderer characterSprite;
    public Color dieColor;
    public Reset_TT resetController;
    public Collider2D thisCollider;
    public Transform particlesTransform;
    public ParticleSystem dieParticles;
    public ParticleSystem.MainModule dieParticlesMinMax;
    public Transform gemsParticlesTransform;
    public ParticleSystem gemsParticles;
    public Gems_TT_Controller gems_TT_Controller;

    [Header("Triggers")]
    public string left;
    public string right;
    public string stick;
    public string jump;
    public string enterHole;
    public string walls;

    public bool dead;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("WARNING:More than one Player_Controller Instance");
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
        //Debug.Log("Start");
        rig.simulated = true;
        this.thisCollider.enabled = true;
        Jump();
    }

    void Update()
    {
        if (dead != true)
        {
            if (Input.GetMouseButtonDown(0) && Time.timeScale == 1 && !onRebotableObject)
            {
                Jump();
            }

            rig.velocity = new Vector2(actualMoveForce, rig.velocity.y - Time.deltaTime * fallSpeed);
        }
    }

    public void Jump()
    {
        audioLevels.JumpAudio();
        spriteAnim.SetTrigger(jump);

        jumpParticles.Play();

        rig.velocity = Vector3.zero;
        rig.AddForce(new Vector2(fowardForce, upForce));
    }

    public void SetOnRebotable(bool _value)
    {
        onRebotableObject = _value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(left))
        {
            fowardForce = -fowardForce;
            actualMoveForce = -actualMoveForce;
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            audioLevels.WallsAudio();

            spriteAnim.SetTrigger(walls);
        }

        if (collision.CompareTag(right))
        {
            fowardForce = -fowardForce;
            actualMoveForce = -actualMoveForce;
            transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
            audioLevels.WallsAudio();

            spriteAnim.SetTrigger(walls);
        }

        if (collision.CompareTag(stick))
        {

            //Die
            Die();
        }
    }

    public void Rebote(float auxForce)
    {
        rig.velocity = Vector3.zero;
        rig.AddForce(new Vector2(fowardForce, auxForce));
        spriteAnim.SetTrigger(jump);
    }

    public void Die()
    {
        /*
         * ANTIGUO DETERMINABA CUANDO MORIA EL JUGADOR POR PRIMERA VEZ
         * 
        if (!PlayerStats.Instance.GetFirstDie())
        {
            PlayerStats.Instance.SetLastDie((ulong)DateTime.Now.Ticks);
            PlayerStats.Instance.SetFistDie(true);
            Data.Instance.SaveData();
        }
        */

        audioLevels.DieAudio();

        characterSprite.color = dieColor;
        thisCollider.enabled = false;
        rig.simulated = false;

        //Active dieParticles
        SetDieParticles();

        //Comprueba si quedan vidas 
        resetController.CheckPlayerLifes();


        SetStartVelocity();
        //ActiveDieSound

        //Desactivar este script
        this.enabled = false;
    }

    public void PlayerInPause()
    {
        audioLevels.DieAudio();

        characterSprite.color = dieColor;
        thisCollider.enabled = false;
        rig.simulated = false;

        //Active dieParticles
        SetDieParticles();
        this.enabled = false;
    }

    public void SetDieParticles()
    {
        particlesTransform.position = this.transform.position;
        dieParticles.Play();

        int particlesAmount = gems_TT_Controller.GetGemsAmount() / 2;

        if (particlesAmount > 15)
            particlesAmount = 15;

        gemsParticlesTransform.position = this.transform.position;
        gemsParticles.emission.SetBurst(0, new ParticleSystem.Burst(0, particlesAmount));
        gemsParticles.Play();
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

    public void EnterHole()
    {
        thisCollider.enabled = false;
        rig.simulated = false;

        spriteAnim.SetTrigger(enterHole);

        this.enabled = false;
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

    public void SetStartMoveForce(float _startMoveForce)
    {
        startMoveForce = _startMoveForce;
    }
}
