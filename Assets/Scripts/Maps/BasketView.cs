using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketView : MonoBehaviour
{
    [SerializeField] private Transform _cubeSpawnPoint;

    public Transform CubeSpawnPoint => _cubeSpawnPoint;
}
