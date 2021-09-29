using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iman_TT : MonoBehaviour
{
    [Header("Ratio SetUp")]
    public float visionRadius;
    public float speed;
    public Transform startPosition;
    public Gem_TT thisGem;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        transform.position = startPosition.position;
    }

    private void Update()
    {
        Vector3 target = transform.position;

        float dist = Vector3.Distance(player.transform.position, transform.position);

        if (dist < visionRadius)
            target = player.transform.position;

        float fixedSpeed = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, fixedSpeed);

        Debug.DrawLine(transform.position, target, Color.green);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
    }

    public Gem_TT GetThisGem()
    {
        return thisGem;
    }

    public void SetStartPos(Transform _startPos)
    {
        startPosition = _startPos;
    }
}
