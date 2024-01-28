using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapState : IState
{
    UnitController unitController;
    float curSpeed;

    public TrapState(UnitController unitController)
    {
        this.unitController = unitController;
    }
    public void Tick()
    {
        curSpeed = Mathf.Min(unitController.navMeshAgent.velocity.magnitude, 1);

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
        unitController.animator.SetFloat("Speed", curSpeed);
        unitController.animator.SetTrigger("Trapped");
        unitController.animator.SetFloat("random_float", animIndex);
        unitController.sceneManager.InstantiateSmoke(unitController.transform.position + Vector3.up);
        unitController.sceneManager.Laugh(4);
    }

    public void OnExit()
    {
        unitController.animator.SetFloat("Speed", curSpeed);
    }


}
