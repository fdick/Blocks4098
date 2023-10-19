using System.Collections;
using System.Collections.Generic;
using Code.Actors;
using UnityEngine;

namespace Code.Factories
{
    public interface ICubeFactory
    {
        public CubeView CreateCube(int cubeID);
    }
}