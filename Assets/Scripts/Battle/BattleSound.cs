using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Battler.Battle
{
    public class BattleSound
    {
        private readonly AudioSource _audioSource;
        private readonly AudioClip _winRoundSound;
        private readonly AudioClip _loseRoundSound;
        private readonly AudioClip _winLevelSound;
        private readonly AudioClip _loseLevelSound;

        public BattleSound(AudioSource audioSource, AudioClip winRoundSound, AudioClip loseRoundSound, AudioClip winLevelSound, AudioClip loseLevelSound)
        {
            _audioSource = audioSource ?? throw new ArgumentNullException(nameof(audioSource));
            _winRoundSound = winRoundSound ?? throw new ArgumentNullException(nameof(winRoundSound));
            _loseRoundSound = loseRoundSound ?? throw new ArgumentNullException(nameof(loseRoundSound));
            _winLevelSound = winLevelSound ?? throw new ArgumentNullException(nameof(winLevelSound));
            _loseLevelSound = loseLevelSound ?? throw new ArgumentNullException(nameof(loseLevelSound));
        }

        public void PlayWinRoundSound() 
        {
            _audioSource.PlayOneShot(_winRoundSound);
        }

        public void PlayLoseRoundSound()
        {
            _audioSource.PlayOneShot(_loseRoundSound);
        }

        public void PlayWinLevelSound()
        {
            _audioSource.PlayOneShot(_winLevelSound);
        }

        public void PlayLoseLevelSound() 
        {
            _audioSource.PlayOneShot(_loseLevelSound);
        }
    }
}
