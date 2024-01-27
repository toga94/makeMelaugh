using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PathManager : Singleton<PathManager>
{
    [SerializeField] private List<PatrolPath> paths;

    private readonly object lockObject = new object(); 

    private void Awake()
    {
        foreach (var path in paths)
        {
            path.IsOccupied = false;
            //path.OnIsOccupiedChanged += OnPathIsOccupiedChanged;
        }

        //UpdateAvailablePaths();

    }

    private void UpdateAvailablePaths()
    {
        foreach (var path in paths)
        {
            if (path.IsOccupied)
            {
                path.IsOccupied = false;
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
            List<int> shuffledIndices = GetShuffledIndices(paths.Count);

            foreach (int index in shuffledIndices)
            {
                if (!paths[index].IsOccupied)
                {
                    paths[index].IsOccupied = true;
                    return paths[index];
                }
            }
        }
        return null;
    }

    private List<int> GetShuffledIndices(int count)
    {
        List<int> indices = new List<int>();
        for (int i = 0; i < count; i++)
        {
            indices.Add(i);
        }

        System.Random random = new System.Random();
        for (int i = 0; i < count - 1; i++)
        {
            int j = random.Next(i, count);
            int temp = indices[i];
            indices[i] = indices[j];
            indices[j] = temp;
        }

        return indices;
    }


    public void ReleasePath(PatrolPath path)
    {
        foreach (var p in paths)
        {
            if(p == path)
                path.IsOccupied = false;
        }

    }
}
