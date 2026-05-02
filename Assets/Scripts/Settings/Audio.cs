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
        private const string VolumeSFX = "SFX";
        private const float MixerMinValue = -80;

        private readonly AudioMixer _audioMixer;

        public Audio(AudioMixer audioMixer)
        {
            _audioMixer = audioMixer ?? throw new ArgumentNullException(nameof(audioMixer));
        }

        public float SoundUI { get; private set; }
        public float SoundSFX { get; private set; }

        public void ApplySavedSettings()
        {
            SetVolumeUI(YG2.saves.soundUI);
            SetVolumeSFX(YG2.saves.soundSFX);
        }

        public void SaveSettings()
        {
            YG2.saves.soundUI = SoundUI;
            YG2.saves.soundSFX = SoundSFX;
            YG2.SaveProgress();
        }

        public void SetVolumeSFX(float value)
        {
            if (value < 0f || value > 1f)
                throw new ArgumentOutOfRangeException(nameof(value));

            SetVolume(value, out float soundSFX, VolumeSFX);
            SoundSFX = soundSFX;
        }

        public void SetVolumeUI(float value)
        {
            if (value < 0f || value > 1f)
                throw new ArgumentOutOfRangeException(nameof(value));

            SetVolume(value, out float soundUI, VolumeUI);
            SoundUI = soundUI;
        }

        private void SetVolume(float value, out float sound, string group)
        {
            sound = value;

            float volume = Mathf.Log10(sound) * DbToPercentConstant;

            if (sound == 0f)
                volume = MixerMinValue;

            _audioMixer.SetFloat(group, volume);
        }
    }
}
