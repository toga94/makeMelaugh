using System;
using UnityEngine;

[Serializable]
public class PatrolPath 
{
    public Transform Transform;
    //public string Name;
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

    public PatrolPath(Transform transform, string name)
    {
        Transform = transform;
        //Name = name;
    }

}
