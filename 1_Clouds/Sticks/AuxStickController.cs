using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuxStickController : MonoBehaviour
{
    //Script auxiliar para controlar los sticks en el nivel de nubes cuando el jugador muera
    //Tambien será el encargado de aumentar o resetear los numeros de sticks que aparecerean en la pantalla

    

    public Animator actualStickAnim;
    public SticksController actualStickController;
    public string exit;

    public void SetActualStick(Animator _newAnimator, SticksController _stickController)
    {
        actualStickAnim = _newAnimator;
        actualStickController = _stickController;
    }

    public void AuxReset()
    {
        actualStickAnim.SetTrigger(exit);
        actualStickController.SetFirstCollision(true);
    }
}
