using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnuncioController : MonoBehaviour
{
    public static AnuncioController Instance;


    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("WARNING:More than one AnuncioController Instance!");
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        AnunciosAdMob.instance.loadRewardVideo();
    }

    public void RewardVideo(string _type)
    {
        AnunciosAdMob.instance.SetRewardType(_type);
        //AnunciosAdMob.instance.CheckReward();
        AnunciosAdMob.instance.showVideoAd();
    }

    public void GamePlayVideo()
    {
        AnunciosAdMob.instance.showVideoAd();
    }
}
