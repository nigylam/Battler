using Battler.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battler.UI
{
    public class SettingsMenu : PopupMenu 
    {
        [SerializeField] private TMP_Dropdown _languageDropDown;
        [SerializeField] private Slider _uiSoundSlider;
        [SerializeField] private Slider _sfxSoundSlider;

        private Language _language;
        private Audio _audio;

        public void Initialize(Language language, Audio audio)
        {
            _language = language;
            _audio = audio;
        }

        protected override void Enable()
        {
            Subscribe();
            _uiSoundSlider.value = _audio.SoundUI;
            _sfxSoundSlider.value = _audio.SoundSFX;
            SetDropdown();
        }

        protected override void Disable()
        {
            Unsubscribe();
        }

        protected override void OnResumeClick()
        {
            _audio.SaveSettings();
            base.OnResumeClick();
        }

        private void OnSFXSoundChanged(float value)
        {
            _audio.SetVolumeSFX(value);
        }

        private void OnUISoundChanged(float value)
        {
            _audio.SetVolumeUI(value);
        }

        private void OnLanguageChanged(int language)
        {
            switch(language)
            {
                case 0:
                    _language.SetLanguage("ru");
                    break;
                case 1:
                    _language.SetLanguage("en");
                    break;
                case 2:
                    _language.SetLanguage("tr");
                    break;
            }
        }

        private void SetDropdown()
        {
            switch (_language.Value)
            {
                case "ru":
                    _languageDropDown.value = 0;
                    break;                
                case "en":
                    _languageDropDown.value = 1;
                    break;                
                case "tr":
                    _languageDropDown.value = 2;
                    break;
            }
        }

        private void Subscribe()
        {
            _languageDropDown.onValueChanged.AddListener(OnLanguageChanged);
            _uiSoundSlider.onValueChanged.AddListener(OnUISoundChanged);
            _sfxSoundSlider.onValueChanged.AddListener(OnSFXSoundChanged);
        }

        private void Unsubscribe()
        {
            _languageDropDown.onValueChanged.RemoveListener(OnLanguageChanged);
            _uiSoundSlider.onValueChanged.RemoveListener(OnUISoundChanged);
            _sfxSoundSlider.onValueChanged.RemoveListener(OnSFXSoundChanged);
        }
    }
}