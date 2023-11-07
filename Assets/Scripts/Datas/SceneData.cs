using Code.Configs;
using Code.Views;
using UnityEngine;

namespace Code.Datas
{
    public class SceneData : MonoBehaviour
    {
        [field: SerializeField] public ActorConfiguration ActorConfiguration { get; private set; }
        [field: SerializeField] public CubesConfig CubesConfiguration { get; private set; }
        [field: SerializeField] public CubeMovementConfiguration CubeMovementConfiguration { get; private set; }
        [field: SerializeField] public CubesPoolConfig CubesPoolConfig { get; private set; }
        [field: SerializeField] public Transform BasketSpawnPoint { get; private set; }
        [field: SerializeField] public CubesStarter CubesStarter { get; private set; }
        [field: SerializeField] public TopPanelView TopPanel { get; private set; }
        [field: SerializeField] public EndPanelView EndPanel { get; private set; }
        [field: SerializeField] public AudioSource BackgroundMusicSource { get; private set; }
        [field: SerializeField] public AudioSource GameplaySoundsSource { get; private set; }
        
        public Transform CubeSpawnPoint { get; set; }
    }
}