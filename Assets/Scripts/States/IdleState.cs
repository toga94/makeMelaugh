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

    }

    public void OnEnter()
    {
        _unitController.animator.SetFloat("Speed", curSpeed);


    }

    public void OnExit()
    {
        _unitController.animator.SetFloat("Speed", curSpeed);

    }


}
