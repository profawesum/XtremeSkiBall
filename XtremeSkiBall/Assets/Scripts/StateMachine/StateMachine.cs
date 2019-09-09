﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Dictionary<Type, BaseState> _availableStates;
    // Start is called before the first frame update

    public BaseState CurrentState { get; private set;}
    public event Action<BaseState> OnStateChanged;

    public void SetStates(Dictionary<Type, BaseState> _states)
    {
        _availableStates = _states;
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentState == null)
        {
            CurrentState = _availableStates.Values.First();
        }

        var nextState = CurrentState?.Tick();

        if(nextState != null && nextState != CurrentState?.GetType())
        {
            SwitchToNewState(nextState);
        }
    }

    private void SwitchToNewState(Type nextState)
    {
        CurrentState = _availableStates[nextState];
        OnStateChanged?.Invoke(CurrentState);
    }
}
