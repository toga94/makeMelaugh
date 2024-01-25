using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitState : IState
{

    UnitController _unitController;
    public SitState(UnitController unitController)
    {
        _unitController = unitController;
    }
    public void FixedTick()
    {
        throw new System.NotImplementedException();
    }

    public void LateTick()
    {
        throw new System.NotImplementedException();
    }

    public void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public void Tick()
    {
        throw new System.NotImplementedException();
    }

}
