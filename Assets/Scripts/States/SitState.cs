using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitState : IState
{

    UnitController _unitController;
    float curSpeed;

    public SitState(UnitController unitController)
    {
        _unitController = unitController;
    }

    public void Tick()
    {

    }
    public void FixedTick()
    {
    }

    public void LateTick()
    {
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }


}
