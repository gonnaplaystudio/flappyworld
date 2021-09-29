using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    public bool right;
    public bool left;
    public Vector3 dir;
    public float speed;
    public float delimitador;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }
    private void Update()
    {
        transform.Translate(dir * Time.deltaTime * speed);
        DeterminateDirection();
    }

    public void DeterminateDirection()
    {
        if (right)
        {
            if (transform.position.x > delimitador)
            {
                transform.position = startPosition;
            }
        }

        if (left)
        {
            if (transform.position.x < delimitador)
            {
                transform.position = startPosition;
            }
        }
    }
}
