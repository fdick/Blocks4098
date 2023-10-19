using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Actors;
using Code.Configs;
using Code.Datas;
using UnityEngine;

namespace Code.Services
{
    public class CubeConfigurationService : ICubeConfigurationService
    {
        public CubeConfigurationService(SceneData sceneData)
        {
            _sceneData = sceneData;
        }

        private SceneData _sceneData;
        private const string CUBE_COLOR = "_CubeColor";
        private const string NUMBER_TEXTURE = "_NumberTex";

        public void Configure(CubeView cube, int index)
        {
            var c = _sceneData.CubesConfiguration.CubesConfigurations[index];
            if (c == null)
                return;

            if (!cube.TryGetComponent<MeshRenderer>(out var meshRenderer))
                return;
            cube.Actor = new CubeActor(c);
            meshRenderer.material.SetColor(CUBE_COLOR, c.Color);
            meshRenderer.material.SetTexture(NUMBER_TEXTURE, c.NumberTex);
        }
    }
}