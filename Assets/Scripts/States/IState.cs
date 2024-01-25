using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IState
{
    void OnEnter();
    void Tick();
    void OnExit();
    void FixedTick();
    void LateTick();
}