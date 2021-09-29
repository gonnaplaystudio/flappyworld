using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideController : MonoBehaviour
{
    public SelectorController selectorController;

    public GameObject buyPanel;
    public GameObject presentationPanel;

    private bool tap, swipeRight, swipeLeft;
    private bool isDraging;
    private Vector2 startTouch, swipeDelta;

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }

    private void Update()
    {
        if (buyPanel.activeInHierarchy || presentationPanel.activeInHierarchy)
            return;

        tap = swipeRight = swipeLeft = false;

        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            SetIsDraging(true);
            startTouch = Input.mousePosition;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            Reset();
        }

        #endregion

        #region Mobile Inputs
        if(Input.touches.Length > 0)
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

        if(isDraging)
        {
            if (Input.touches.Length > 0)
                swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }

        //Did we cross the deadzone?
        if(swipeDelta.magnitude > 200)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //left or right
                if (x < 0)
                {
                    swipeLeft = true;                    
                    selectorController.RightButton();
                }
                else
                {
                    swipeRight = true;
                    selectorController.LeftButton();
                }
                    
            }

            Reset();
        }
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
}
