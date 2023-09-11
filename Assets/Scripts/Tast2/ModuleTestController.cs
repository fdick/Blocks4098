using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleTestController : MonoBehaviour
{
    private IPathfinder _pathfinder;
    private void Start()
    {
        _pathfinder = new PathfindUtility();
        
        var edges = new Edge[]
        {
            new Edge() { Start = new Vector3(4, 4), End = new Vector3(6, 4) },
            new Edge() { Start = new Vector3(13, 4), End = new Vector3(13, 6) },
        };
        var A = new Vector3(1, 0);
        var C = new Vector3(20, 3);

        _pathfinder.GetPath(A, C, edges);

    }
}