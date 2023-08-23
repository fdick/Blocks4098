using UnityEngine;

public class AudioProcess : MonoBehaviour
{
    [SerializeField] private AudioSource _mainSource;
    [SerializeField] private AudioClip _shoot;
    [SerializeField] private AudioClip _takeDamage;

    public AudioClip Shoot => _shoot;
    public AudioClip TakeDamage => _takeDamage;

    public void Play(AudioClip clip)
    {
        if(_mainSource.isPlaying)
            _mainSource.Stop();
        _mainSource.clip = clip;
        _mainSource.Play();
    }
}
