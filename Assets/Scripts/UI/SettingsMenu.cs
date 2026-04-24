using Battler.Localization;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battler.UI
{
    public class SettingsMenu : PopupMenu 
    {
        [SerializeField] private TMP_Dropdown _languageDropDown;

        private Language _language;

        public void Initialize(Language language)
        {
            _language = language;
        }

        protected override void Enable()
        {
            base.Enable();
            _languageDropDown.onValueChanged.AddListener(OnLanguageChanged);
        }

        protected override void Disable()
        {
            base.Disable();
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