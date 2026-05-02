using UnityEngine;
using UnityEngine.Audio;
using System;
using YG;

namespace Battler.Settings
{
    public class Audio
    {
        private const int DbToPercentConstant = 20;
        private const string VolumeUI = "UI";
        private const float MixerMinValue = -80;

        private readonly AudioMixer _audioMixer;

        private float _soundUI;

        public Audio(AudioMixer audioMixer)
        {
            _audioMixer = audioMixer ?? throw new ArgumentNullException(nameof(audioMixer));
        }

        public void ApplySavedSettings()
        {
            SetVolumeUI(YG2.saves.soundUI);
        }

        public void SaveSettings()
        {
            YG2.saves.soundUI = _soundUI;
            YG2.SaveProgress();
        }

        public void SetVolumeUI(float value)
        {
            if (value < 0f || value > 1f)
                throw new ArgumentOutOfRangeException(nameof(value));

            _soundUI = value;

            float volume = Mathf.Log10(_soundUI) * DbToPercentConstant;

            if (_soundUI == 0f)
                volume = MixerMinValue;

            _audioMixer.SetFloat(VolumeUI, volume);
        }
    }
}
