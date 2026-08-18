using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.BattleSystem.Units
{
    public class Sound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _damageSounds;
        [SerializeField] private AudioClip[] _actionSounds;
        [SerializeField] private AudioClip[] _deathSounds;
        [SerializeField] private float _pitchOffset;

        private float _defaultPitch;

        private void Start()
        {
            _defaultPitch = _audioSource.pitch;
        }

        public void PlayDamageSound()
        {
            if (_damageSounds.Length == 0)
                return;

            Play(_damageSounds);
        }

        public void PlayActionSound()
        {
            Play(_actionSounds);
        }

        public void PlayDeathSound()
        {
            Play(_deathSounds);
        }

        private void Play(AudioClip[] audioClips)
        {
            _audioSource.pitch = Random.Range(_defaultPitch - _pitchOffset, _defaultPitch + _pitchOffset);
            _audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
            _audioSource.Play();
        }
    }
}
