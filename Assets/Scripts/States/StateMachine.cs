using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    List<StateTransformer> _stateTransformers = new();
    List<StateTransformer> _anyStateTransformers = new();

    IState _currentState;
    IState _previousState;

    public IState CurrentState => _currentState;
    public IState PreviousState => _previousState;

    public void SetState(IState state)
    {
        if (_currentState == state) return;

        _currentState?.OnExit();
        _previousState = _currentState;
        _currentState = state;
        _currentState.OnEnter();
    }

    public void Tick()
    {
        StateTransformer stateTransformer = CheckForTransformationOfState();

        if (stateTransformer != null)
        {

            SetState(stateTransformer.To);
        }

        _currentState.Tick();
    }

    public void FixedTick()
    {
        _currentState.FixedTick();
    }

    public void LateTick()
    {
        _currentState.LateTick();

    }

    private StateTransformer CheckForTransformationOfState()
    {
        foreach (StateTransformer stateTransformer in _anyStateTransformers)
        {
            if (stateTransformer.Condition.Invoke()) return stateTransformer;
        }

        foreach (StateTransformer stateTransformer in _stateTransformers)
        {
            if (stateTransformer.Condition.Invoke() && _currentState == stateTransformer.From) return stateTransformer;
        }

        return null;
    }

    public void AddState(IState from, IState to, System.Func<bool> condition)
    {
        StateTransformer stateTransformer = new StateTransformer(from, to, condition);
        _stateTransformers.Add(stateTransformer);
    }

    public void AddAnyState(IState to, System.Func<bool> condition)
    {
        StateTransformer anystateTransformer = new StateTransformer(null, to, condition);
        _anyStateTransformers.Add(anystateTransformer);
    }

}