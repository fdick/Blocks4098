using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _mainSource;
    [SerializeField] private AudioClip _mainMusic;

    private void Start()
    {
        _mainSource.clip = _mainMusic;
        _mainSource.Play();
    }
}