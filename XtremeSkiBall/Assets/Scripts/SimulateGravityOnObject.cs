﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulateGravityOnObject : MonoBehaviour
{
    const float GravConst = 500000;

    public static List<SimulateGravityOnObject> ObjectsEffectedByGravity;
    bool hasGravity = false;

    public Rigidbody rb;

    private void FixedUpdate()
    {
        if (hasGravity)
        {
            foreach (SimulateGravityOnObject gravityOnObject in ObjectsEffectedByGravity)
            {
                if (gravityOnObject != this)
                    Attract(gravityOnObject);
            } 
        }
    }

    private void OnEnable()
    {
        if (this.tag == "gravBallThrown")
            hasGravity = true;
        if(ObjectsEffectedByGravity == null)
        {
            ObjectsEffectedByGravity = new List<SimulateGravityOnObject>();
        }
        ObjectsEffectedByGravity.Add(this);
    }

    private void OnDisable()
    {
        ObjectsEffectedByGravity.Remove(this);
    }

    void Attract(SimulateGravityOnObject objToAttract)
    {
        Rigidbody rbToAttract = objToAttract.rb;

        Vector3 direction = rb.position - rbToAttract.position;
        float distance = direction.magnitude;

        if (distance == 0)
            return; // Distance shouldn't be this

        float forceMagnitude = GravConst * (rb.mass * rbToAttract.mass) * 10000 / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        rbToAttract.AddForce(force);
    }
}
