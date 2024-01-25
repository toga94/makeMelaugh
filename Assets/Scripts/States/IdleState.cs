using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{

    UnitController _unitController;
    float curSpeed;
    public IdleState(UnitController unitController)
    {
        _unitController = unitController;
    }
    public void FixedTick()
    {
    }
    public void Tick()
    {
        curSpeed = Mathf.Min(_unitController.navMeshAgent.velocity.magnitude, 1);
    }

    public void LateTick()
    {
        _unitController.animator.SetFloat("Speed", curSpeed);

    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }


}
