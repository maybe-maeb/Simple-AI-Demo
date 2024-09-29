using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ControllerState
{
    protected StateController controller;

    public abstract void CheckTransitions();

    public abstract void Act();

    public virtual void OnStateEnter() { }

    public virtual void OnStateExit() { }

    public ControllerState(StateController controller){
        this.controller = controller;
    }
}