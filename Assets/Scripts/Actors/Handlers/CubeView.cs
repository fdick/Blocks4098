using System;
using System.Collections;
using System.Collections.Generic;
using Code.Actors;
using Code.Datas;
using UnityEngine;
using Zenject;

namespace Code.Actors
{
    [RequireComponent(typeof(MeshRenderer))]
    public class CubeView : MonoBehaviour
    {
        private CubeActor _actor;

        private MeshRenderer _meshRenderer;
        private Material _material;
        private Transform _transform;

        public CubeActor Actor
        {
            get => _actor;
            set => _actor = value;
        }

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _material = _meshRenderer.material;
            _transform = transform;
        }
    }
}