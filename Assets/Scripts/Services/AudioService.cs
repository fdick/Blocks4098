using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Services
{
    public class AudioService
    {
        public AudioService(AudioSource bgrMusicSource, AudioSource gameplaySoundsSource)
        {
            _bgrMusicSource = bgrMusicSource;
            _gameplaySoundsSource = gameplaySoundsSource;
        }

        private AudioSource _bgrMusicSource;
        private AudioSource _gameplaySoundsSource;


        public void PlaySound(AudioClip clip, bool loop = false)
        {
            if (_gameplaySoundsSource.isPlaying)
                _gameplaySoundsSource.Stop();
            _gameplaySoundsSource.clip = clip;
            _gameplaySoundsSource.loop = loop;
            _gameplaySoundsSource.Play();
        }

        public void PlayMusic(AudioClip clip = null, bool loop = false)
        {
            if (_bgrMusicSource.isPlaying)
                _bgrMusicSource.Stop();
            if (clip != null)
                _bgrMusicSource.clip = clip;
            _bgrMusicSource.loop = loop;
            _bgrMusicSource.Play();
        }
    }
}