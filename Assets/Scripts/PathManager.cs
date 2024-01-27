using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PathManager : Singleton<PathManager>
{
    [SerializeField] private List<PatrolPath> paths;

    public List<PatrolPath> availablePatrolPaths { get; private set; }

    private readonly object lockObject = new object(); 

    private void Awake()
    {
        availablePatrolPaths = new List<PatrolPath>();

        foreach (var path in paths)
        {
            //path.Name = path.Transform.name;
            path.IsOccupied = false;
            path.OnIsOccupiedChanged += OnPathIsOccupiedChanged;
        }

        UpdateAvailablePaths();

    }

    private void UpdateAvailablePaths()
    {
        availablePatrolPaths.Clear();
        foreach (var path in paths)
        {
            if (!path.IsOccupied)
            {
                availablePatrolPaths.Add(path);
            }
        }
    }

    private void OnPathIsOccupiedChanged(PatrolPath path)
    {
        lock (lockObject)
        {
            UpdateAvailablePaths();
        }
    }

    public PatrolPath RequestPath()
    {
        lock (lockObject)
        {

            UpdateAvailablePaths();
            for (int i = 0; i < availablePatrolPaths.Count; i++)
            {
                int index = Random.Range(0, availablePatrolPaths.Count);
                
                availablePatrolPaths[index].IsOccupied = true;
                return availablePatrolPaths[index];
                
            }
            //foreach (var path in availablePatrolPaths)
            //{
            //    if (!path.IsOccupied)
            //    {
            //        path.IsOccupied = true;
            //        return path;
            //    }
            //}
        }
        return null; 
    }

    public void ReleasePath(PatrolPath path)
    {
        lock (lockObject)
        {
            path.IsOccupied = false;
        }
    }
}
