using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTut : MonoBehaviour
{
    public Transform actualTransform;
    public float speed;

    private Vector3 dir;

    private void Update()
    {

        Vector3 objectPos = new Vector3(actualTransform.position.x, 
            transform.position.y,transform.position.z);

        transform.position = Vector3.Lerp(transform.position, objectPos,speed * Time.deltaTime);

    }

    public void ChangeActualTranform(Transform _actualTransform)
    {
        actualTransform = _actualTransform;
    }

    public void PlayerFollow()
    {
        //followPLayer = true;
    }
}
