using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IState
{

    UnitController _unitController;
    float curSpeed;
    public WalkState(UnitController unitController)
    {
        _unitController = unitController;
    }
    public void Tick()
    {
        curSpeed = Mathf.Min(_unitController.navMeshAgent.velocity.magnitude, 1);

    }
    public void FixedTick()
    {
        //_unitController.navMeshAgent.SetDestination(_unitController.patrolPoints[currentPatrolIndex].transform.position);

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
