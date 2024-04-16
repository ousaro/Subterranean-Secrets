using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class ElevatorMvt : MonoBehaviour
{


    private float speed = 1f;
    private int direction = 1;
    [SerializeField] Transform targetPos, startPos;

    [SerializeField] int id;


    // Update is called once per frame
    void Update()
    {
        PlatformMvt();

    }

    private void PlatformMvt()
    {
        Vector2 target = CurrentMvtTarget();

        transform.position = Vector2.Lerp(transform.position, target, speed * Time.deltaTime);

        float distance = (target - (Vector2)transform.position).magnitude;

        if (distance < 0.1f)
        {
            direction *= -1;
        }
    }

    Vector2 CurrentMvtTarget()
    {
        if (direction == 1)
        {
            return startPos.position;
        }
        else
        {
            return targetPos.position;
        }
      
 
    }

    public int Id() { return id; }  
}
