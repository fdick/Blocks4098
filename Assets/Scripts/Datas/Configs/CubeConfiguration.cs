using System;
using UnityEngine;

namespace Code.Configs
{
    [Serializable]
    public class CubeConfiguration
    {
        [field: SerializeField] public int Index { get; private set; }
        [field: SerializeField] public Color Color { get; private set; }
        [field: SerializeField] public Texture NumberTex { get; private set; }

        public int GetDenomitaion() => (int)Mathf.Pow(2, Index + 1);
    }
}