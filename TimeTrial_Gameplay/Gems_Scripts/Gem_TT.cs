using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem_TT : MonoBehaviour
{
    public int gemValue;
    public string gemSpireAtlasValue;

    public AudioInLevels audioLevels;
    public Gems_TT_Controller gems_TT_Controller;

    public ParticleSystem[] particlesValue;

    public void SetGemValue(string _atlasValue,int _gemValue)
    {
        gemValue = _gemValue;
        gemSpireAtlasValue = _atlasValue;
    }

    public string GemSpriteAtlasValue()
    {
        return gemSpireAtlasValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioLevels.GemAudio();
            //Se añadira la puntuacion
            gems_TT_Controller.AddGem(gemValue);
            //Se instanciaran las particulas de value
            ShowValue();
            //Se desactivara la actual gema
            Desactive();
        }
    }

    public void Desactive()
    {
        gems_TT_Controller.CheckDesactive();
        this.transform.parent.gameObject.SetActive(false);
    }

    public void ShowValue()
    {
        switch (gemValue)
        {
            case 1:
                SetParticles(particlesValue[0]);
                break;

            case 2:
                SetParticles(particlesValue[1]);
                break;

            case 3:
                SetParticles(particlesValue[2]);
                break;
        }
    }

    public void SetParticles(ParticleSystem _particles)
    {
        _particles.gameObject.transform.position = this.transform.position;
        _particles.Play();
    }

    public void SetValue_1(ParticleSystem _particle)
    {
        particlesValue[0] = _particle;
    }

    public void SetValue_2(ParticleSystem _particle)
    {
        particlesValue[1] = _particle;
    }

    public void SetValue_3(ParticleSystem _particle)
    {
        particlesValue[2] = _particle;
    }
}
