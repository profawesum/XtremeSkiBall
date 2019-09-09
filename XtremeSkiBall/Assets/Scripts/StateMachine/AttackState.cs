using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private AI aI;
    public AttackState(AI _aI) : base(_aI.gameObject)
    {
        aI = _aI;
    }
    public override Type Tick()
    {
        aI.FireWeapon();
        return typeof(WanderState);
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
