using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.RemoteConfig;
using UnityEngine.SceneManagement;

public class RemoteConfig : MonoBehaviour
{
    public struct userAttributes { }
    public struct appAttributes { }

    public RemoteConfData remoteConfData;

    public bool done;
    private int counter;

    private void Awake()
    {
        ConfigManager.FetchCompleted += SetRemoteConfig;
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }

    public void SetRemoteConfig(ConfigResponse response)
    {
        switch (response.requestOrigin)
        {
            case ConfigOrigin.Default:

                Debug.Log("No update, default values");

                break;

            case ConfigOrigin.Cached:

                Debug.Log("No update, previus sesion values");

                break;

            case ConfigOrigin.Remote:

                Debug.Log("New Rules");

                remoteConfData.SetCapName(ConfigManager.appConfig.GetString("Cap_Name"));
                remoteConfData.SetArcadeLevel(ConfigManager.appConfig.GetInt("arcadeLevel"));
                remoteConfData.SetCounterLevel(ConfigManager.appConfig.GetInt("counterLevel"));
                remoteConfData.SetBossLevel(ConfigManager.appConfig.GetInt("bossLevel"));
                remoteConfData.SetVSLevel(ConfigManager.appConfig.GetBool("VS_Level"));
                remoteConfData.SetLeftFlappy(ConfigManager.appConfig.GetString("leftFlappy"));
                remoteConfData.SetRightFlappy(ConfigManager.appConfig.GetString("rightFlappy"));
                remoteConfData.SetLeftFlappyLink(ConfigManager.appConfig.GetString("leftFlappy_link"));
                remoteConfData.SetRightFlappyLink(ConfigManager.appConfig.GetString("rightFlappy_link"));
                remoteConfData.SetFlappyOfTheWeek(ConfigManager.appConfig.GetString("flappy_of_the_week"));

                done = ConfigManager.appConfig.GetBool("Done");
                break;
        }  
    }

    private void Start()
    {
        //Invoke("ChargeIntro", 1f);

        InvokeRepeating("CheckInternet", 0f, .1f);
    }

    public void CheckInternet()
    {
        //Primero comprueba si hay internet e inicio la cuentra atras por si hay que cargar en default
        if (done)
        {
            ChargeIntro();

        }else if(counter >= 60)
        {
            ChargeDefault();
            ChargeIntro();
        }

        counter++;
    }       

    public void ChargeIntro()
    {
        SceneManager.LoadScene(1);
    }

    public void ChargeDefault()
    {
        remoteConfData.SetCapName("Default");
        remoteConfData.SetArcadeLevel(0);
        remoteConfData.SetCounterLevel(2);
        remoteConfData.SetBossLevel(1);
        remoteConfData.SetVSLevel(false);
        remoteConfData.SetLeftFlappy("Flusty");
        remoteConfData.SetRightFlappy("Flalt Bae");
        remoteConfData.SetLeftFlappyLink("https://www.instagram.com/p/CJI3DWgKBcr/");
        remoteConfData.SetRightFlappyLink("https://www.instagram.com/p/CJI29RKKjbU/");
        remoteConfData.SetFlappyOfTheWeek("Flarbossa");
    }

    private void OnDestroy()
    {
        ConfigManager.FetchCompleted -= SetRemoteConfig;
    }
}
