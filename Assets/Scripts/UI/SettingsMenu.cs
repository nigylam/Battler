using Battler.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Battler.UI
{
    public class SettingsMenu : PopupMenu 
    {
        [SerializeField] private TMP_Dropdown _languageDropDown;
        [SerializeField] private Slider _uiSoundSlider;

        private Language _language;
        private Audio _audio;

        public void Initialize(Language language, Audio audio)
        {
            _language = language;
            _audio = audio;
        }

        protected override void Enable()
        {
            _languageDropDown.onValueChanged.AddListener(OnLanguageChanged);
            _uiSoundSlider.onValueChanged.AddListener(OnUiSoundValueChanged);
            _uiSoundSlider.value = YG2.saves.soundUI;
        }

        protected override void Disable()
        {
            _languageDropDown.onValueChanged.RemoveListener(OnLanguageChanged);
            _uiSoundSlider.onValueChanged.RemoveListener(OnUiSoundValueChanged);
        }

        protected override void OnResumeClick()
        {
            _audio.SaveSettings();
            base.OnResumeClick();
        }

        private void OnUiSoundValueChanged(float value)
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
    }
}