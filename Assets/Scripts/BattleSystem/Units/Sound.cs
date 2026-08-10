using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.BattleSystem.Units
{
    public class Sound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _damageSounds;
        [SerializeField] private AudioClip[] _attackSounds;
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

        public void PlayAttackSound()
        {
            Debug.Log("PlayAttackSound");
            Play(_attackSounds);
        }

        private void Play(AudioClip[] audioClips)
        {
            Debug.Log("Play");
            _audioSource.pitch = Random.Range(_defaultPitch - _pitchOffset, _defaultPitch + _pitchOffset);
            _audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
            _audioSource.Play();
        }
    }
}
