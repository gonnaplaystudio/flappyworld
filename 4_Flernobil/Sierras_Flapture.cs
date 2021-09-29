using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sierras_Flapture : MonoBehaviour
{
    public Transform target;
    public Transform startPos;
    public float speed;

    private Vector3 dir;

    private void Update()
    {
        dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.05f)
        {
            SetStartPos();
            this.gameObject.SetActive(false);
        }
    }

    public void SetStartPos()
    {
        transform.position = startPos.position;
    }
}
