using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds_Rebotables : MonoBehaviour
{
    private Animator anim;
    public string trigger;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger(trigger);
        }
    }
}
