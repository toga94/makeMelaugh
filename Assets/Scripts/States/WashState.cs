using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashState : IState
{
    UnitController _unitController;
    float curSpeed;

    public WashState(UnitController unitController)
    {
        _unitController = unitController;
    }

    public void Tick()
    {
        curSpeed = Mathf.Min(_unitController.navMeshAgent.velocity.magnitude, 1);

    }
    public void FixedTick()
    {
        _unitController.transform.rotation = _unitController.Target.Transform.rotation;
    }

    public void LateTick()
    {

    }

    public void OnEnter()
    {
        _unitController.animator.SetFloat("Speed", curSpeed);
        _unitController.animator.SetBool("IsWashing", true);
    }

    public void OnExit()
    {
        _unitController.animator.SetBool("IsWashing", false);
        _unitController.animator.SetFloat("Speed", curSpeed);
    }


}
