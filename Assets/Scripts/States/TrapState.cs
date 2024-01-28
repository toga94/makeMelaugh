using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapState : IState
{
    UnitController _unitController;
    float curSpeed;

    public TrapState(UnitController unitController)
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
        int animIndex = Random.Range(0, 4);
        _unitController.animator.SetFloat("Speed", curSpeed);
        _unitController.animator.SetTrigger("Trapped");
        _unitController.animator.SetFloat("random_float", animIndex);
    }

    public void OnExit()
    {
        _unitController.animator.SetFloat("Speed", curSpeed);
    }


}
