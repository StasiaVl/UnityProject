﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public Vector3 MoveBy;
    public double time_to_wait;
    public float speed;

    Vector3 pointA;
    Vector3 pointB;

    double time;
    Vector3 my_pos;
    Vector3 target;

    // Use this for initialization
    void Start()
    {
        this.pointA = this.transform.position;
        this.pointB = pointA + MoveBy;
        target = this.pointB;
        time = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (time <= 0)
        {
            my_pos = this.transform.position;
            Vector3 destination = target - my_pos;
            destination.z = 0;
            if (isArrived(my_pos, target))
            {
                time = time_to_wait;
                if (target == this.pointA)
                    target = this.pointB;
                else target = this.pointA;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed);
            }
        }
        else
        {
            time -= Time.deltaTime;
        }
    }

    bool isArrived(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) < 0.02f;
    }

}
