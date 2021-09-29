using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public bool start;
    public float waitTime;

    public MonoBehaviour player_Controller;
    public GameObject player;

    public Animator playerAnim;
    public Animator holeAnim;
    public string holeTrigger;

    private void Awake()
    {
        Analiticas.Instance.FlappyPlayedAmount();
    }

    private void Start()
    {
        PlayerStats.Instance.SetNavigationFade(false);
        //Se esperara a la animacion fade para activar el start game la primera vez que se entre en escena 
        Invoke("StartTheGameFirstTime", 1f);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && start)
        {
            player_Controller.enabled = true;
            playerAnim.enabled = false;
            SetStart(false);

            this.enabled = false;
        }
    }

    public void LetGameStart()
    {
        SetStart(true);
    }

    public void SetStart(bool _start)
    {
        start = _start;
    }

    public void ActiveHole()
    {
        holeAnim.SetTrigger(holeTrigger);
    }

    public void ActivePlayer()
    {
        player.SetActive(true);
    }

    public void StartTheGame()
    {
        Invoke("LetGameStart", waitTime);
        ActiveHole();
    }

    public void StartTheGameFirstTime()
    {
        Invoke("LetGameStart", waitTime);
        ActiveHole();
        ActivePlayer();
    }

    public void TutStart()
    {
        player_Controller.enabled = true;
        playerAnim.enabled = false;
        SetStart(false);
        this.enabled = false;
    }
}
