using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private EnvironmentHandler _envMovement;

    private void Start()
    {
        _envMovement.StartEnvMovement();
        _spawner.StartSpawner();
    }

    private void OnDestroy()
    {
    }
}
