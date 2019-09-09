using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    private AI aI;
    public ChaseState(AI _aI) : base(_aI.gameObject)
    {
        aI = _aI;
    }
    public override Type Tick()
    {
        aI.planes = GeometryUtility.CalculateFrustumPlanes(aI.AiCam);
        if (GeometryUtility.TestPlanesAABB(aI.planes, aI.playerColls[aI.currentTarget].bounds))
        {
            Debug.Log("Player is in camera Box");
            //RayCast Test to see if its behind a wall.
            Vector3 dirToTarget = (aI.Players[aI.currentTarget].transform.position - transform.position).normalized;
            float dstToTarget = Vector3.Distance(transform.position, aI.Players[aI.currentTarget].transform.position);
            if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, aI.obstacleMask))
            {
                aI.Agent.SetDestination(aI.Players[aI.currentTarget].transform.position);
                aI.LastSeenPosition = aI.Players[aI.currentTarget].transform.position;
                //if in range then it should go to attack else chase after player
                if(dstToTarget < aI.FiringRange)
                return typeof(AttackState);
            }                
        }

        //Behind a wall. Head to last seen location
        aI.Agent.SetDestination(aI.LastSeenPosition);
        if (Vector3.Distance(transform.position, aI.LastSeenPosition) < aI.PickupDistance)
        {
            return typeof(WanderState);
        }
        return typeof(ChaseState);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
