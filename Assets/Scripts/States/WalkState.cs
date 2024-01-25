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
        if (!_unitController.navMeshAgent.hasPath || _unitController.navMeshAgent.velocity.sqrMagnitude == 0f)
        {
            //navMeshAgent.SetDestination(RandomNavmeshLocation(walkRadius));

            if (_unitController.stayTimer < _unitController.stayDuration)
            {
                _unitController.stayTimer += Time.deltaTime;
            }
            else
            {
                _unitController.SetNextPatrolPoint();
            }
        }

        curSpeed = Mathf.Min(_unitController.navMeshAgent.velocity.magnitude, 1);

    }
    public void FixedTick()
    {

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
