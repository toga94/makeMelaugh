using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookState : IState
{
    UnitController _unitController;
    float curSpeed;

    public CookState(UnitController unitController)
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
    }

    public void OnEnter()
    {
        _unitController.animator.SetFloat("Speed", curSpeed);
        _unitController.animator.SetBool("IsCooking", true);
    }

    public void OnExit()
    {
        _unitController.animator.SetBool("IsCooking", false);
        _unitController.animator.SetFloat("Speed", curSpeed);
    }



}
