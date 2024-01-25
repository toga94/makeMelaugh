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
        curSpeed = Mathf.Min(_unitController.navMeshAgent.velocity.magnitude, 1);

    }
    public void FixedTick()
    {
    }

    public void LateTick()
    {
        _unitController.animator.Play("Sit");
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
