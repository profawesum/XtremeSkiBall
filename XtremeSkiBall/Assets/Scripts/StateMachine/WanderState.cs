using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class WanderState : BaseState
{
    private static int CurrentLocation;
    private Vector3? _destination;
    private AI aI;

    public WanderState(AI _aI) : base(_aI.gameObject)
    {
        aI = _aI;
        CurrentLocation = Random.Range(0, aI.WeaponPositions.Count);
    }

    public override Type Tick()
    {
        aI.planes = GeometryUtility.CalculateFrustumPlanes(aI.AiCam);
        if (aI.hasWeapon == true)
        {
            for (int i = 0; i < aI.Players.Length; i++)
            {// Checks if it's in the cam
                if (GeometryUtility.TestPlanesAABB(aI.planes, aI.playerColls[i].bounds))
                {
                    Debug.Log("Player is in camera Box");
                    //RayCast Test to see if its behind a wall.
                    Vector3 dirToTarget = (aI.Players[i].transform.position - transform.position).normalized;
                    float dstToTarget = Vector3.Distance(transform.position, aI.Players[i].transform.position);
                    if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, aI.obstacleMask))
                    {
                        aI.SetTarget(aI.Players[i].transform, i);
                        return typeof(ChaseState);
                    }
                }
            }
            //Hunting movement is here if you want to have patrol path to hunt down players

        }
        //Getting weapon movement is here
        Debug.Log("Getting Weapon");
        if(Vector3.Distance(transform.position, aI.WeaponPositions[CurrentLocation].transform.position) < aI.PickupDistance)
        {
            CurrentLocation = Random.Range(0, aI.WeaponPositions.Count);     
        }
        aI.Agent.SetDestination(aI.WeaponPositions[CurrentLocation].transform.position);
        return typeof(WanderState);

    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
