using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SticksController : MonoBehaviour
{
    //Script integrado en los muros
        
    public AuxStickController auxStickController;

    public SticksController otherStickController;

    public string enter;
    public string exit;

    public Animator animEnter;
    public Animator animExit;

    public bool firstCollision;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (firstCollision)
        {
            animEnter.SetTrigger(enter);
            SetFirstCollision(false);
            auxStickController.SetActualStick(animEnter, otherStickController);
            return;
        }

        animExit.SetTrigger(exit);
        animEnter.SetTrigger(enter);

        auxStickController.SetActualStick(animEnter, otherStickController);
    }

    public void SetFirstCollision(bool _bool)
    {
        firstCollision = _bool;
    }
}
