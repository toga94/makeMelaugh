using System;
using UnityEngine;

[Serializable]
public class PatrolPath 
{
    public Transform Transform;
    private bool isOccupied = false;
    public bool IsOccupied
    {
        get { return isOccupied; }
        set
        {
            if (isOccupied != value)
            {
                isOccupied = value;
                OnIsOccupiedChanged?.Invoke(this);
            }
        }
    }
    public event Action<PatrolPath> OnIsOccupiedChanged;


}
