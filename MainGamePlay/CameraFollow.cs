using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform playerTransform;
    public float speed;

    private void Update()
    {
        /*
        transform.position = new Vector3(playerTransform.position.x,
            transform.position.y, transform.position.z);



        Vector3 dir = new Vector3(playerTransform.position.x - transform.position.x, 0f, 0f);

        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        */
        transform.position = new Vector3(playerTransform.position.x,
            transform.position.y, transform.position.z);
        transform.rotation = playerTransform.rotation;

    }
}
