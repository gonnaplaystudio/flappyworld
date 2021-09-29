using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulo : MonoBehaviour
{
    public Rigidbody2D rig;
    public float leftPushRange;
    public float rightPushRange;
    public float velocityThreshol;
    public float startVelocity;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();

        rig.angularVelocity = startVelocity;
    }

    private void Update()
    {
        Moving();
    }

    public void Moving()
    {
        if(transform.rotation.z > 0 && transform.rotation.z
            < rightPushRange && (rig.angularVelocity > 0) &&
            rig.angularVelocity < velocityThreshol)
        {
            rig.angularVelocity = velocityThreshol;

        }else if(transform.rotation.z < 0 && transform.rotation.z 
            > leftPushRange && (rig.angularVelocity < 0) &&
            rig.angularVelocity > velocityThreshol * -1)
        {
            rig.angularVelocity = velocityThreshol * -1;
        }
    }
}
