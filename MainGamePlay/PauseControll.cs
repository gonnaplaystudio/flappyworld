using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseControll : MonoBehaviour
{
    public static PauseControll Instance;

    [Header("Pause SetUp")]
    public GameObject pauseCanvas;
    public Animator thisAnim;
    public Animator scoreAnim;
    public string scoreExitTrigger;
    public bool TT_Gameplay;
    public bool bossLevel;
    public bool VS_level;

    private bool isActive;

    private bool tap, swipeDown, swipeUp;
    private bool isDraging;
    private Vector2 startTouch, swipeDelta;

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Warning: More than one PauseControll Instance");
            return;
        }

        Instance = this;

        Invoke("IsActivePause", 2f);
    }

    private void Update()
    {
        if (!isActive)
            return;

        EscButton();

        tap = swipeDown = swipeUp = false;

        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            SetIsDraging(true);
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Reset();
        }

        #endregion

        #region Mobile Inputs
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                SetIsDraging(true);
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                Reset();
            }
        }

        #endregion

        //Calculate distance
        swipeDelta = Vector2.zero;

        if (isDraging)
        {
            if (Input.touches.Length > 0)
                swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }

        //Did we cross the deadzone?
        if (swipeDelta.magnitude > 200)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) < Mathf.Abs(y))
            {
                //left or right
                if (y < 0)
                {
                    swipeDown = true;
                    //Se reanuda el juego
                    ResumeGame();
                }
                else
                {
                    swipeUp = true;
                    //Se pausa el juego
                    PauseGame();
                }

            }

            Reset();
        }
    }

    public void IsActivePause()
    {
        isActive = true;
    }

    public void Reset()
    {
        SetIsDraging(false);
        startTouch = swipeDelta = Vector2.zero;
    }

    public void SetIsDraging(bool _value)
    {
        isDraging = _value;
    }

    public void PauseGame()
    {
        if(Time.timeScale != 0)
        {
            Time.timeScale = 0;
            pauseCanvas.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pauseCanvas.SetActive(false);
        }
    }

    public void EscButton()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if(Time.timeScale != 0)
            {
                Time.timeScale = 0;
                pauseCanvas.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                pauseCanvas.SetActive(false);
            }
        }
    }

    public void MenuButton()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;

            if (TT_Gameplay)
            {
                Player_Timer_Trial.Instance.PlayerInPause();
            }
            else if(bossLevel)
            {
                Player_Boss.Instance.PlayerInPause();
            }
            else if(VS_level)
            {
                Player_VS player_vs = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<Player_VS>();
                VS_Player vs_player = GameObject.FindGameObjectWithTag("IA").GetComponent<VS_Player>();

                player_vs.PlayerInPause();
                vs_player.PlayerInPause();
            }
            else
            {
                Player_Controller.Instance.PlayerInPause();
            }
            
            thisAnim.SetTrigger("Exit");
            Invoke("LoadLevelsScene", 1f);
            PlayerStats.Instance.SetFadeFromLevels(true);
            DesactiveScore();
        }        
    }

    public void LoadLevelsScene()
    {
        SceneManager.LoadScene(3);
    }

    public void DesactiveScore()
    {
        scoreAnim.SetTrigger(scoreExitTrigger);
    }
}
